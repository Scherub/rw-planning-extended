using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal class PlanDoorDesignationPlacer : BasePlanDesignationPlacer
    {
        protected override PlanDesignationType Type => PlanDesignationType.PlanDoors;

        protected override RotationDirection GetRotation(Map map, IntVec3 position)
        {
            Orientation orientation = GetOrientation(map, position);

            return orientation == Orientation.Vertical ? RotationDirection.Clockwise : RotationDirection.None;
        }

        protected override void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {
            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(map, position);
        }

        Orientation GetOrientation(Map map, IntVec3 position)
        {
            PlanDesignation planDesignationNorth = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.North);
            PlanDesignation planDesignationSouth = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.South);
            PlanDesignation planDesignationWest = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.West);
            PlanDesignation planDesignationEast = map.designationManager.GetOnlyPlanDesignationAt(position + IntVec3.East);

            if (planDesignationWest?.IsDoorOrWall == true || planDesignationEast?.IsDoorOrWall == true)
                return Orientation.Horizontal;

            if (planDesignationNorth?.IsDoorOrWall == true || planDesignationSouth?.IsDoorOrWall == true)
                return Orientation.Vertical;

            if (planDesignationNorth?.IsFloor == true || planDesignationSouth?.IsFloor == true)
                return Orientation.Horizontal;

            if (planDesignationWest?.IsFloor == true || planDesignationEast?.IsFloor == true)
                return Orientation.Vertical;

            return Orientation.Horizontal;
        }
    }
}
