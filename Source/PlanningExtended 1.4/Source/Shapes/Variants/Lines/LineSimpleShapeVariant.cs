using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Lines
{
    internal class LineSimpleShapeVariant : BaseShapeGeneratorVariant<LineGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.LineSimple;

        public override ShapeOptions SecondShapeOption => ShapeOptions.Thickness;

        public LineSimpleShapeVariant()
            : base(new LineShapeModifier(), new LineGenerator())
        {
        }
    }
}
