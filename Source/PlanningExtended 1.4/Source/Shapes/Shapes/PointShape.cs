using PlanningExtended.Shapes.Variants.Points;

namespace PlanningExtended.Shapes
{
    internal class PointShape : BaseShape
    {
        public override Shape Shape => Shape.Point;

        public PointShape()
            : base(ShapeVariant.Points, new PointShapeVariant())
        {
        }
    }
}
