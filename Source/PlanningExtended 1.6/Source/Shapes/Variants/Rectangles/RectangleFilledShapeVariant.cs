using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleFilledShapeVariant : BaseShapeGeneratorVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleFilled;

        public RectangleFilledShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(true), new RotationShapeFeature(Direction.Horizontal, Direction.Horizontal | Direction.Diagonal))
        {
        }
    }
}
