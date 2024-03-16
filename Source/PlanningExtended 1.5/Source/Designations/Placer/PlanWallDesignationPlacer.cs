using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal class PlanWallDesignationPlacer : BasePlanDesignationPlacer
    {
        protected override PlanDesignationType Type => PlanDesignationType.PlanWall;

        protected override void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, position);
        }
    }
}
