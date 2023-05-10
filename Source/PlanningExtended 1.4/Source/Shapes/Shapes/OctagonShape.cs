using PlanningExtended.Shapes.Variants.Octagons;

namespace PlanningExtended.Shapes
{
    internal class OctagonShape : BaseShape
    {
        public override Shape Shape => Shape.Octagon;

        public OctagonShape()
            : base(ShapeVariant.OctagonOutline, new OctagonOutlineShapeVariant())
        {
        }
    }
}
