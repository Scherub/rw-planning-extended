using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Pentagons
{
    internal class PentagonFilledShapeVariant : BaseShapeGeneratorVariant<PentagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.PentagonFilled;

        public PentagonFilledShapeVariant()
            : base(new SquareShapeModifier(), new PentagonGenerator(true), new RotationShapeFeature(Direction.North, Direction.MainAxes))
        {
        }
    }
}
