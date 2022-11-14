using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Lines
{
    public class ThreeLinesShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.ThreeLines;
        
        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
