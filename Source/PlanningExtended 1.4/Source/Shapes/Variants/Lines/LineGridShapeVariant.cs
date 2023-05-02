using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Lines
{
    internal class LineGridShapeVariant : BaseShapeGeneratorVariant<OldRectangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.LineGrid;

        public LineGridShapeVariant()
            : base(new SquareShapeModifier(), new OldRectangleGenerator(false, false, true, false), new SegmentShapeFeature(true))
        {
        }
    }
}
