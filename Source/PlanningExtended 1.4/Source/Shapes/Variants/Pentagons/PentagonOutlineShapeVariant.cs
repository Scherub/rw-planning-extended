using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Pentagons
{
    internal class PentagonOutlineShapeVariant : BaseShapeGeneratorVariant<HexagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.OctagonOutline;

        public PentagonOutlineShapeVariant()
            : base(new SquareShapeModifier(), new HexagonGenerator(false))
        {
        }
    }
}
