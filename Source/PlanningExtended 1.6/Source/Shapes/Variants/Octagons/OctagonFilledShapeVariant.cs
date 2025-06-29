using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Octagons
{
    internal class OctagonFilledShapeVariant : BaseShapeGeneratorVariant<OctagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.OctagonFilled;

        public OctagonFilledShapeVariant()
            : base(new SquareShapeModifier(), new OctagonGenerator(true))
        {
        }
    }
}
