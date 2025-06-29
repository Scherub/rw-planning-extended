using PlanningExtended.Shapes.Variants.Ellipses;

namespace PlanningExtended.Shapes
{
    internal class EllipseShape : BaseShape
    {
        public override Shape Shape => Shape.Ellipse;

        public EllipseShape()
            : base(ShapeVariant.EllipseFilled, new EllipseFilledShapeVariant(), new EllipseOutlineShapeVariant())
        {
        }
    }
}
