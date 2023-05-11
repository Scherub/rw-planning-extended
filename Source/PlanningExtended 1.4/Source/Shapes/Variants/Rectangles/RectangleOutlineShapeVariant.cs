using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleOutlineShapeVariant : BaseShapeGeneratorVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleOutline;

        public RectangleOutlineShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(false), new RotationShapeFeature(Direction.Horizontal, Direction.Horizontal | Direction.DiagonalAll))
        {
        }
    }
}
