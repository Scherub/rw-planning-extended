using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Ellipses
{
    internal class EllipseOutlineShapeVariant : BaseShapeGeneratorVariant<EllipseGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.EllipseOutline;

        //public override ShapeOptions SecondShapeOption => ShapeOptions.Thickness;

        public EllipseOutlineShapeVariant()
            : base(new SquareShapeModifier(), new EllipseGenerator(true, false))
        {

        }
    }
}
