using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Quadrangles
{
    internal class QuadrangleFilledShapeVariant : BaseShapeGeneratorVariant<QuadrangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.QuadrangleFilled;

        public QuadrangleFilledShapeVariant()
            : base(new SquareShapeModifier(), new QuadrangleGenerator(true), new RotationShapeFeature(Direction.Horizontal, Direction.Horizontal | Direction.Diagonal))
        {
        }
    }
}
