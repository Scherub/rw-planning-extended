using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using PlanningExtended.Plans;
using PlanningExtended.Settings;
using PlanningExtended.UndoRedo;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace PlanningExtended.Gravships;

/// <summary>
/// Transfers plans with a gravship from the takeoff map to the landing map. Positions are stored relative to the grav engine and rotated accordingly.
/// </summary>
internal static class GravshipPlanTransfer
{
    const string SaveKey = "planningExtendedPlans";

    // Keeps the data alive exactly as long as the gravship itself.
    static readonly ConditionalWeakTable<Gravship, GravshipPlanData> planData = [];

    public static void Capture(Gravship gravship)
    {
        try
        {
            CaptureInternal(gravship);
        }
        catch (Exception ex)
        {
            Log.Error($"[Planning Extended] Failed to capture gravship plans: {ex}");
        }
    }

    public static void Restore(Gravship gravship, IntVec3 root, Map map)
    {
        try
        {
            RestoreInternal(gravship, root, map);
        }
        catch (Exception ex)
        {
            Log.Error($"[Planning Extended] Failed to restore gravship plans: {ex}");
        }
    }

    public static void ExposeData(Gravship gravship)
    {
        GravshipPlanData data = Get(gravship);

        if (Scribe.mode == LoadSaveMode.Saving && data == null)
            return;

        Scribe_Deep.Look(ref data, SaveKey);

        if (Scribe.mode == LoadSaveMode.LoadingVars)
            Set(gravship, data);
    }

    static void CaptureInternal(Gravship gravship)
    {
        Building_GravEngine engine = gravship.Engine;
        Map map = engine?.Map;

        if (map == null)
            return;

        PlanningSettings settings = PlanningMod.Settings;
        IntVec3 origin = gravship.originalPosition;
        HashSet<IntVec3> substructure = engine.ValidSubstructure;

        GravshipPlanData data = new() { TransferArea = settings.gravshipPlanTransferArea };

        foreach (Thing thing in gravship.Things)
            if (thing.Spawned && thing.TryGetComp(out CompSubstructureFootprint footprint) && footprint.Valid)
                data.Footprints.Add(new GravshipFootprint(thing.Position - origin, footprint.Props.radius));

        HashSet<IntVec3> rangeCells = [.. substructure, .. GetFootprintCells(map, data.Footprints, origin, Rot4.North)];

        List<IntVec3> removedCells = [];

        foreach (IntVec3 cell in rangeCells)
        {
            PlanDesignation planDesignation = map.designationManager.GetOnlyPlanDesignationAt(cell);

            if (planDesignation == null)
                continue;

            PlanDesignationType planDesignationType = PlanDesignationTypeConverter.Convert(planDesignation.def.defName);

            if (planDesignationType == PlanDesignationType.Unknown)
                continue;

            PlanCell planCell = new((cell - origin).ToIntVec2, planDesignationType, planDesignation.colorDef?.defName);

            data.RangeCells.Add(planCell);

            bool isOnSubstructure = substructure.Contains(cell);
            bool isTransferred = isOnSubstructure || data.TransferArea == GravshipPlanTransferArea.GravshipRange;

            if (isTransferred)
                data.TransferCells.Add(planCell);

            // Plans on the substructure are always moved with ship launch, others only if they are not kept.
            if (isOnSubstructure || (isTransferred && !settings.keepGravshipRangePlansOnTakeoff))
            {
                map.designationManager.RemovePlanDesignationsAt(cell);
                removedCells.Add(cell);
            }
        }

        foreach (IntVec3 cell in removedCells)
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, cell);

        // Undo-redo snapshots use absolute positions and would no longer match after the ship moved.
        UndoRedoManager.Clear();

        if (data.RangeCells.Count > 0)
            Set(gravship, data);
    }

    static void RestoreInternal(Gravship gravship, IntVec3 root, Map map)
    {
        GravshipPlanData data = Get(gravship);

        if (data == null || map == null)
            return;

        Set(gravship, null);

        Rot4 rotation = gravship.Rotation;
        HashSet<IntVec3> clearedCells = [];

        if (PlanningMod.Settings.gravshipLandingPlanMode == GravshipLandingPlanMode.Clear)
        {
            HashSet<IntVec3> landingArea = [.. gravship.Foundations.Keys.Select(local => root + local)];

            if (data.TransferArea == GravshipPlanTransferArea.GravshipRange)
                landingArea.UnionWith(GetFootprintCells(map, data.Footprints, root, rotation));

            foreach (IntVec3 cell in landingArea)
                if (cell.InBounds(map) && map.designationManager.RemovePlanDesignationsAt(cell))
                    clearedCells.Add(cell);
        }

        List<IntVec3> placedCells = [];

        foreach (PlanCell planCell in data.TransferCells)
        {
            IntVec3 position = root + PrefabUtility.GetAdjustedLocalPosition(planCell.Position.ToIntVec3, rotation);

            if (!position.InBounds(map))
                continue;

            DesignationDef designationDef = PlanDesignationUtilities.GetDesignationDef(planCell.Designation);
            ColorDef colorDef = ColorUtilities.GetColorDefByName(planCell.Color);

            PlanDesignationPlacerUtilities.Designate(map, position, designationDef, colorDef);

            placedCells.Add(position);
        }

        // Door and wall orientation depends on neighbours, which may have been placed later.
        foreach (IntVec3 cell in placedCells)
            PlanDesignationPlacerUtilities.Update(map, cell);

        foreach (IntVec3 cell in clearedCells)
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, cell);

        CopyToClipboard(data.RangeCells, rotation);
    }

    static void CopyToClipboard(List<PlanCell> planCells, Rot4 rotation)
    {
        if (planCells.Count == 0)
            return;

        List<(IntVec3 position, PlanCell planCell)> rotatedCells = [.. planCells.Select(c => (PrefabUtility.GetAdjustedLocalPosition(c.Position.ToIntVec3, rotation), c))];

        IntVec3 min = new(rotatedCells.Min(c => c.position.x), 0, rotatedCells.Min(c => c.position.z));

        List<PlanCell> clipboardCells = [.. rotatedCells.Select(c => new PlanCell((c.position - min).ToIntVec2, c.planCell.Designation, c.planCell.Color))];

        Plans.PlanManager.SetCachedPlanLayout(new PlanLayout(clipboardCells), false);

        Messages.Message("PlanningExtended.GravshipPlansCopied".Translate(), MessageTypeDefOf.NeutralEvent, false);
    }

    static IEnumerable<IntVec3> GetFootprintCells(Map map, IEnumerable<GravshipFootprint> footprints, IntVec3 root, Rot4 rotation)
    {
        foreach (GravshipFootprint footprint in footprints)
        {
            IntVec3 center = root + PrefabUtility.GetAdjustedLocalPosition(footprint.Offset, rotation);

            foreach (IntVec3 cell in GenRadial.RadialCellsAround(center, footprint.Radius, true))
                if (cell.InBounds(map))
                    yield return cell;
        }
    }

    static GravshipPlanData Get(Gravship gravship)
    {
        return planData.TryGetValue(gravship, out GravshipPlanData data) ? data : null;
    }

    static void Set(Gravship gravship, GravshipPlanData data)
    {
        planData.Remove(gravship);

        if (data != null)
            planData.Add(gravship, data);
    }
}
