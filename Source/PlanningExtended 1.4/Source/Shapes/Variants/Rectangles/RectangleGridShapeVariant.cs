using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleGridShapeVariant : BaseShapeSegmentsVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleGrid;

        public override ShapeOptions FirstShapeOption => ShapeOptions.NumberOfSegmentsZ;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegmentsX;

        public RectangleGridShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(true, false, true, false), true)
        {
        }
    }
}
