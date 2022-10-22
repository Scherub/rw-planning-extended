using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes
{
    public class SubdividedRectangle : BaseShape
    {
        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            if (cell.x == areaDimensions.MinX || cell.x == areaDimensions.MaxX)
                return true;

            if (cell.z == areaDimensions.MinZ || cell.z == areaDimensions.MaxZ)
                return true;

            return false;
        }
    }
}
