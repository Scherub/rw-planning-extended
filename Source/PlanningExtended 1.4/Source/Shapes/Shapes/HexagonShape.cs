using PlanningExtended.Shapes.Variants.Hexagons;

namespace PlanningExtended.Shapes
{
    internal class HexagonShape : BaseShape
    {
        public override Shape Shape => Shape.Hexagon;

        public HexagonShape()
            : base(ShapeVariant.HexagonFilled, new HexagonFilledShapeVariant(), new HexagonOutlineShapeVariant())
        {
        }
    }
}
