using PlanningExtended.Shapes.Variants.Triangles;

namespace PlanningExtended.Shapes
{
    internal class TriangleShape : BaseShape
    {
        public override Shape Shape => Shape.Triangle;

        public TriangleShape()
            : base(ShapeVariant.TriangleOutline, new TriangleOutlineShapeVariant())
        {
        }
    }
}
