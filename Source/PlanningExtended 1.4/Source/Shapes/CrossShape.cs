using PlanningExtended.Cells;
using PlanningExtended.Shapes.Options;
using Verse;

namespace PlanningExtended.Shapes
{
    public class CrossShape : BaseShape<NullShapeOptions>
    {
        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            IntVec3 center = areaDimensions.Center;

            if (cell.x == center.x)
                return true;

            if (cell.z == center.z)
                return true;

            return false;
        }
    }
}
