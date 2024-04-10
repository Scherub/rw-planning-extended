using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Ellipses
{
    internal class EllipseOutlineShapeVariant : BaseShapeGeneratorVariant<EllipseGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.EllipseOutline;

        public EllipseOutlineShapeVariant()
            : base(new SquareShapeModifier(), new EllipseGenerator(false))
        {
        }
    }
}
