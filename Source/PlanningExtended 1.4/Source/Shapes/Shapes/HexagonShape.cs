using PlanningExtended.Shapes.Variants.Hexagons;

namespace PlanningExtended.Shapes
{
    internal class HexagonShape : BaseShape
    {
        public HexagonShape()
            : base(ShapeVariant.HexagonOutline, new HexagonOutlineShapeVariant())
        {
        }
    }
}
