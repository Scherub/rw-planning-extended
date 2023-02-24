using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleOutlineShapeVariant : BaseShapeGeneratorVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleOutline;

        public RectangleOutlineShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(true, false, false, false))
        {
        }
    }
}
