using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Lines
{
    public class TwoLinesShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.TwoLines;

        public override ShapeOptions FirstShapeOption => ShapeOptions.Rotation;

        //public override ShapeOptions SecondShapeOption => ShapeOptions.Thickness;

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
