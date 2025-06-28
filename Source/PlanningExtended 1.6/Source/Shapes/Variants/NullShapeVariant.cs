using PlanningExtended.Cells;
using PlanningExtended.Shapes.Modifiers.Dimensions;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    internal class NullShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.Unknown;

        public NullShapeVariant()
            : base(new NullShapeModifier())
        {
        }

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
