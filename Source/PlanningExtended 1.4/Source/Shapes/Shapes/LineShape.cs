using PlanningExtended.Shapes.Variants.Lines;

namespace PlanningExtended.Shapes
{
    internal class LineShape : BaseShape
    {
        public override Shape Shape => Shape.Line;

        public LineShape()
            : base(ShapeVariant.LineSimple, new LineSimpleShapeVariant(), new LineGridShapeVariant())
        {
        }
    }
}
