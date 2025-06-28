using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Triangles
{
    internal class TriangleFilledShapeVariant : BaseShapeGeneratorVariant<TriangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.TriangleFilled;

        public TriangleFilledShapeVariant()
            : base(new EquilateralTriangleShapeModifier(), new TriangleGenerator(true), new RotationShapeFeature(Direction.North, Direction.MainAxes))
        {
        }
    }
}
