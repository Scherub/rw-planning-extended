using PlanningExtended.Cells;
using PlanningExtended.Shapes.Options;
using Verse;

namespace PlanningExtended.Shapes
{
    public class Rectangle : BaseShape<NullShapeOptions>
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
