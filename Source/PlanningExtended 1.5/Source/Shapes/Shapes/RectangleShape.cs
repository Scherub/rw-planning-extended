using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes
{
    internal class RectangleShape : BaseShape
    {
        public override Shape Shape => Shape.Rectangle;

        public RectangleShape()
            : base(ShapeVariant.RectangleFilled, new RectangleFilledShapeVariant(), new RectangleGridShapeVariant(), new RectangleOutlineShapeVariant())
        {
        }
    }
}
