using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal class PlanObjectDesignationPlacer : BasePlanDesignationPlacer
    {
        protected override PlanDesignationType Type => PlanDesignationType.PlanObjects;

        protected override void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {
            if (removedPlanDesignation)
                PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, position);
        }
    }
}
