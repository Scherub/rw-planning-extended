using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Pentagons
{
    internal class PentagonOutlineShapeVariant : BaseShapeGeneratorVariant<PentagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.PentagonOutline;

        public PentagonOutlineShapeVariant()
            : base(new SquareShapeModifier(), new PentagonGenerator(false), new RotationShapeFeature(Direction.North, Direction.MainAxes))
        {
        }
    }
}
