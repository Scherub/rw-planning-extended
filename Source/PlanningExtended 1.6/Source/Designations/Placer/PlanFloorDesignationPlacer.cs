using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal class PlanFloorDesignationPlacer : BasePlanDesignationPlacer
    {
        protected override PlanDesignationType Type => PlanDesignationType.PlanFloors;

        protected override void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, position);
        }
    }
}
