using System.Collections.Generic;
using PlanningExtended.Defs;
using PlanningExtended.Designations.Placer;
using RimWorld;
using Verse;

namespace PlanningExtended.Designations
{
    internal static class PlanDesignationPlacerUtilities
    {
        readonly static Dictionary<PlanDesignationType, BasePlanDesignationPlacer> planDesignationPlacers = new()
        {
            { PlanDesignationType.PlanDoors, new PlanDoorDesignationPlacer() },
            { PlanDesignationType.PlanFloors, new PlanFloorDesignationPlacer() },
            { PlanDesignationType.PlanObjects, new PlanObjectDesignationPlacer() },
            { PlanDesignationType.PlanWall, new PlanWallDesignationPlacer() }
        };

        public static void Designate(Map map, IntVec3 position, DesignationDef designationDef, ColorDef colorDef)
        {
            PlanDesignationType planDesignationType = DesignationDefUtilities.GetType(designationDef);

            if (planDesignationPlacers.TryGetValue(planDesignationType, out BasePlanDesignationPlacer planDesignationPlacer))
                planDesignationPlacer.Designate(map, position, designationDef, colorDef);
        }

        public static void UpdateAdjecentPositions(Map map, IntVec3 position)
        {
            foreach (var adjecentPosition in GetAdjecent(position))
                Update(map, adjecentPosition);
        }

        public static void Update(Map map, IntVec3 position)
        {
            PlanDesignation planDesignation = map.designationManager.GetOnlyPlanDesignationAt(position);

            if (planDesignation != null)
                if (planDesignationPlacers.TryGetValue(planDesignation.PlanType, out BasePlanDesignationPlacer planDesignationPlacer))
                    planDesignationPlacer.Update(map, position);
        }

        static IntVec3[] GetAdjecent(IntVec3 position)
        {
            return new[] { position + IntVec3.North, position + IntVec3.South, position + IntVec3.East, position + IntVec3.West };
        }
    }
}
