using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Lines;

internal class LineSimplePaddedShapeVariant : BaseShapeGeneratorVariant<LineSimpleGenerator>
{
    public override ShapeVariant ShapeVariant => ShapeVariant.LineSimplePadded;

    public LineSimplePaddedShapeVariant()
        : base(new LineSimpleShapeModifier(), new LineSimpleGenerator(true))
    {
    }
}
