using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes
{
    internal class QuadrangleShape : BaseShape
    {
        public QuadrangleShape()
            : base(ShapeVariant.RectangleFilled, new RectangleFilledShapeVariant(), new RectangleGridShapeVariant(), new RectangleOutlineShapeVariant())
        {
        }
    }
}
