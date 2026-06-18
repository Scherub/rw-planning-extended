using PlanningExtended.Shapes.Variants.Fill;

namespace PlanningExtended.Shapes;

internal class FillShape : BaseShape
{
    public override Shape Shape => Shape.Fill;

    public FillShape()
        : base(ShapeVariant.Fill,
              new FillShapeVariant())
    {
    }
}
