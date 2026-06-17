using PlanningExtended.Shapes.Variants.FillColor;

namespace PlanningExtended.Shapes;

internal class FillColorShape : BaseShape
{
    public override Shape Shape => Shape.FillColor;

    public FillColorShape()
        : base(ShapeVariant.FillColorAll,
              new FillColorAllShapeVariant(),
              new FillColorSameShapeVariant())
    {
    }
}
