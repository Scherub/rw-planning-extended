using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Quadrangles
{
    internal class QuadrangleOutlineShapeVariant : BaseShapeGeneratorVariant<QuadrangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.QuadrangleOutline;

        public QuadrangleOutlineShapeVariant()
            : base(new SquareShapeModifier(), new QuadrangleGenerator(false), new RotationShapeFeature(Direction.Horizontal, Direction.Horizontal | Direction.Diagonal))
        {
        }
    }
}
