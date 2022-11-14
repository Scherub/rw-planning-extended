using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Ellipses
{
    public class FilledEllipseShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.FilledEllipse;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
