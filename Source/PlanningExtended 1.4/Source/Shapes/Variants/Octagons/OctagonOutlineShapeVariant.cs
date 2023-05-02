using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Octagons
{
    internal class OctagonOutlineShapeVariant : BaseShapeGeneratorVariant<OctagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.OctagonOutline;

        public OctagonOutlineShapeVariant()
            : base(new SquareShapeModifier(), new OctagonGenerator(false))
        {
        }
    }
}
