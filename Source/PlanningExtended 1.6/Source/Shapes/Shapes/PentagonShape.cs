using PlanningExtended.Shapes.Variants.Pentagons;

namespace PlanningExtended.Shapes
{
    internal class PentagonShape : BaseShape
    {
        public override Shape Shape => Shape.Pentagon;

        public PentagonShape()
            : base(ShapeVariant.PentagonFilled, new PentagonFilledShapeVariant(), new PentagonOutlineShapeVariant())
        {
        }
    }
}
