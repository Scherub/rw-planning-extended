using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal class PlanDoorDesignationPlacer : BasePlanDesignationPlacer
    {
        protected override PlanDesignationType Type => PlanDesignationType.PlanDoors;

        protected override RotationDirection GetRotation(Map map, IntVec3 position)
        {
            bool isRotated = MustBeRotated(map, position);

            return isRotated ? RotationDirection.Clockwise : RotationDirection.None;
        }

        protected override void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, position);
        }

        bool MustBeRotated(Map map, IntVec3 position)
        {
            PlanDesignation planDesignationNorth = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.North);
            PlanDesignation planDesignationSouth = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.South);
            PlanDesignation planDesignationWest = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.West);
            PlanDesignation planDesignationEast = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.East);

            if (planDesignationWest?.IsDoorOrWall == true || planDesignationEast?.IsDoorOrWall == true)
                return false;

            if (planDesignationNorth?.IsDoorOrWall == true || planDesignationSouth?.IsDoorOrWall == true)
                return true;

            return false;
        }
    }
}
