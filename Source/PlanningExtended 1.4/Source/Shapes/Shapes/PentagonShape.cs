using PlanningExtended.Shapes.Variants.Pentagons;

namespace PlanningExtended.Shapes
{
    internal class PentagonShape : BaseShape
    {
        public PentagonShape()
            : base(ShapeVariant.PentagonOutline, new PentagonOutlineShapeVariant())
        {
        }
    }
}
