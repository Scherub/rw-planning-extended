using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Lines
{
    internal class LineSimpleShapeVariant : BaseShapeGeneratorVariant<LineSimpleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.LineSimple;

        public LineSimpleShapeVariant()
            : base(new LineSimpleShapeModifier(), new LineSimpleGenerator())
        {
        }
    }
}
