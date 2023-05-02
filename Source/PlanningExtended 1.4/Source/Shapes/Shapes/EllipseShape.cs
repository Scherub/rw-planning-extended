using PlanningExtended.Shapes.Variants.Ellipses;

namespace PlanningExtended.Shapes
{
    internal class EllipseShape : BaseShape
    {
        public EllipseShape()
            : base(ShapeVariant.EllipseOutline, new EllipseOutlineShapeVariant(), new EllipseFilledShapeVariant())
        {
        }
    }
}
