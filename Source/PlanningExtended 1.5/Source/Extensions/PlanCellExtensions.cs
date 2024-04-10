using PlanningExtended.Plans;
using Verse;

namespace PlanningExtended
{
    public static class PlanCellExtensions
    {
        public static PlanCell Flip(this PlanCell planCell, FlipDirection flipDirection, IntVec2 areaSize)
        {
            IntVec2 position = planCell.Position.Flip(flipDirection, areaSize);

            return new PlanCell(position, planCell.Designation, planCell.Color);
        }

        public static PlanCell Rotate(this PlanCell planCell, RotationDirection rotationDirection)
        {
            IntVec2 position = planCell.Position.Rotate(rotationDirection);

            return new PlanCell(position, planCell.Designation, planCell.Color);
        }

        public static PlanCell Rotate(this PlanCell planCell, RotationDirection rotationDirection, IntVec2 offset)
        {
            IntVec2 position = planCell.Position.Rotate(rotationDirection, offset);

            return new PlanCell(position, planCell.Designation, planCell.Color);
        }
    }
}
