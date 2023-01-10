using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Lines
{
    internal class LineGridShapeVariant : BaseShapeSegmentsVariant<RectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.LineGrid;

        public override ShapeOptions FirstShapeOption => ShapeOptions.NumberOfSegmentsZ;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegmentsX;

        public LineGridShapeVariant()
            : base(new SquareShapeModifier(), new RectangleGenerator(false, false, true, false), true)
        {
        }
    }
}
