using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class RectangleGridShapeVariant : BaseShapeSegmentsVariant<OldRectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.RectangleGrid;

        public override ShapeOptions FirstShapeOption => ShapeOptions.NumberOfSegmentsZ;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegmentsX;

        public RectangleGridShapeVariant()
            : base(new SquareShapeModifier(), new OldRectangleGenerator(true, false, true, false), true)
        {
        }
    }
}
