using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Points
{
    internal class PointShapeVariant : BaseShapeSegmentsVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.Points;

        public override ShapeOptions FirstShapeOption => ShapeOptions.NumberOfSegmentsZ;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegmentsX;


        public PointShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(false, false, false, true), false)
        {
        }
    }
}
