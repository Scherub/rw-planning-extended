using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleRoundedOutlineShapeVariant : BaseShapeGeneratorVariant<RectangleRoundedGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleRoundedOutline;

        public RectangleRoundedOutlineShapeVariant()
            : base(new SquareShapeModifier(), new RectangleRoundedGenerator(false))
        {
        }
    }
}
