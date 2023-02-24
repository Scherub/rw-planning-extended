using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleFilledShapeVariant : BaseShapeGeneratorVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleFilled;

        public RectangleFilledShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(true, true, false, false))
        {
        }
    }
}
