using PlanningExtended.Shapes.Variants.Octagons;

namespace PlanningExtended.Shapes
{
    internal class OctagonShape : BaseShape
    {
        public OctagonShape()
            : base(ShapeVariant.OctagonOutline, new OctagonOutlineShapeVariant())
        {
        }
    }
}
