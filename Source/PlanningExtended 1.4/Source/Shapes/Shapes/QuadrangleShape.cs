using PlanningExtended.Shapes.Variants.Quadrangles;
using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes
{
    internal class QuadrangleShape : BaseShape
    {
        public override Shape Shape => Shape.Quadrangle;

        public QuadrangleShape()
            : base(ShapeVariant.RectangleFilled, new RectangleFilledShapeVariant(), new RectangleGridShapeVariant(), new RectangleOutlineShapeVariant(), new QuadrangleOutlineShapeVariant())
        {
        }
    }
}
