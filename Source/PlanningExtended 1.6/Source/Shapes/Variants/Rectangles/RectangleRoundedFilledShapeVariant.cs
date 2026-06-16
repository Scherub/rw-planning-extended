using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleRoundedFilledShapeVariant : BaseShapeGeneratorVariant<RectangleRoundedGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleRoundedFilled;

        public RectangleRoundedFilledShapeVariant()
            : base(new SquareShapeModifier(), new RectangleRoundedGenerator(true))
        {
        }
    }
}
