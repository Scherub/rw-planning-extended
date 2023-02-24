using PlanningExtended.Shapes.Variants.Points;

namespace PlanningExtended.Shapes
{
    internal class PointShape : BaseShape
    {
        public PointShape()
            : base(ShapeVariant.Points, new PointShapeVariant())
        {
        }
    }
}
