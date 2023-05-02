using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants.Triangles
{
    internal class TriangleOutlineShapeVariant : BaseShapeGeneratorVariant<TriangleGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.TriangleOutline;

        public TriangleOutlineShapeVariant()
            : base(new EquilateralTriangleShapeModifier(), new TriangleGenerator(false), new RotationShapeFeature(Direction.North, Direction.MainAxes))
        {
        }
    }
}
