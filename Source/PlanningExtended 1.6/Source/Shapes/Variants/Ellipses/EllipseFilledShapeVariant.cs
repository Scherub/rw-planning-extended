using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Ellipses
{
    internal class EllipseFilledShapeVariant : BaseShapeGeneratorVariant<EllipseGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.EllipseFilled;

        public EllipseFilledShapeVariant()
            : base(new SquareShapeModifier(), new EllipseGenerator(true))
        {
        }
    }
}
