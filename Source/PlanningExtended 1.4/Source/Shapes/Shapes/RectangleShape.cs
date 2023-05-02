using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes
{
    internal class RectangleShape : BaseShape
    {
        public RectangleShape()
            : base(ShapeVariant.RectangleFilled, new RectangleOutlineShapeVariant(), new RectangleGridShapeVariant(), new RectangleFilledShapeVariant())
        {
        }
    }
}
