using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleFilledShapeVariant : BaseShapeGeneratorVariant<OldRectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleFilled;

        public RectangleFilledShapeVariant()
            : base(new SquareShapeModifier(), new OldRectangleGenerator(true, true, false, false))
        {
        }
    }
}
