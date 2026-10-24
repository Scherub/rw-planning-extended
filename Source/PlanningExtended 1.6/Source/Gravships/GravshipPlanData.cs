using System.Collections.Generic;
using PlanningExtended.Plans;
using Verse;

namespace PlanningExtended.Gravships;

/// <summary>
/// Plans carried by a gravship. All positions are relative to the grav engine at takeoff, in the takeoff orientation.
/// </summary>
public class GravshipPlanData : IExposable
{
    GravshipPlanTransferArea transferArea;
    public GravshipPlanTransferArea TransferArea
    {
        get => transferArea;
        set => transferArea = value;
    }

    List<GravshipFootprint> footprints = [];
    public List<GravshipFootprint> Footprints => footprints;

    // All plans within gravship range, copied to the clipboard on landing.
    List<PlanCell> rangeCells = [];
    public List<PlanCell> RangeCells => rangeCells;

    // Plans that are placed again on landing.
    List<PlanCell> transferCells = [];
    public List<PlanCell> TransferCells => transferCells;

    public void ExposeData()
    {
        Scribe_Values.Look(ref transferArea, nameof(transferArea));
        Scribe_Collections.Look(ref footprints, nameof(footprints), LookMode.Deep);
        Scribe_Collections.Look(ref rangeCells, nameof(rangeCells), LookMode.Deep);
        Scribe_Collections.Look(ref transferCells, nameof(transferCells), LookMode.Deep);

        if (Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            transferCells ??= [];
            rangeCells ??= [];
            footprints ??= [];
        }
    }
}
