using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Hexagons
{
    internal class HexagonFilledShapeVariant : BaseShapeGeneratorVariant<HexagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.HexagonFilled;

        public HexagonFilledShapeVariant()
            : base(new RegularHexagonShapeModifier(), new HexagonGenerator(true), new RotationShapeFeature(Direction.Horizontal, Direction.MainAxes))
        {
        }
    }
}
