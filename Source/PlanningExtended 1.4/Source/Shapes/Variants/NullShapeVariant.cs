using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    public class NullShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.Unknown;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
