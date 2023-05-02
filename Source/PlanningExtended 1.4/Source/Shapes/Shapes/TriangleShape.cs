using PlanningExtended.Shapes.Variants.Triangles;

namespace PlanningExtended.Shapes
{
    internal class TriangleShape : BaseShape
    {
        public TriangleShape()
            : base(ShapeVariant.TriangleOutline, new TriangleOutlineShapeVariant())
        {
        }
    }
}
