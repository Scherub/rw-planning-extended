using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleGridShapeVariant : BaseShapeGeneratorVariant<OldRectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleGrid;

        public RectangleGridShapeVariant()
            : base(new SquareShapeModifier(), new OldRectangleGenerator(true, false, true, false), new SegmentShapeFeature(true))
        {
        }
    }
}
