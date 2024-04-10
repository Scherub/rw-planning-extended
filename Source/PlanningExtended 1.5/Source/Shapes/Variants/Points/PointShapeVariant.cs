using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Points
{
    internal class PointShapeVariant : BaseShapeGeneratorVariant<PointGridGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.Points;

        public PointShapeVariant()
            : base(new SquareShapeModifier(), new PointGridGenerator(), new SegmentShapeFeature(false))
        {
        }
    }
}
