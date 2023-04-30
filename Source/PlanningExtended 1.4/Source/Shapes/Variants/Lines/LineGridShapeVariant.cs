using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Lines
{
    internal class LineGridShapeVariant : BaseShapeSegmentsVariant<OldRectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.LineGrid;

        public override ShapeOptions FirstShapeOption => ShapeOptions.NumberOfSegmentsZ;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegmentsX;

        public LineGridShapeVariant()
            : base(new SquareShapeModifier(), new OldRectangleGenerator(false, false, true, false), true)
        {
        }
    }
}
