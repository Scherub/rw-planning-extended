using PlanningExtended.Shapes.Variants.Lines;

namespace PlanningExtended.Shapes
{
    internal class LineShape : BaseShape
    {
        public LineShape()
            : base(ShapeVariant.LineSimple, new LineSimpleShapeVariant(), new LineGridShapeVariant())
        {
        }
    }
}
