using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes;

internal class RectangleShape : BaseShape
{
    public override Shape Shape => Shape.Rectangle;

    public RectangleShape()
        : base(ShapeVariant.RectangleFilled,
            new RectangleOutlineShapeVariant(),
            new RectangleGridShapeVariant(),
            new RectangleFilledShapeVariant(),
            new RectangleRoundedOutlineShapeVariant(),
            new RectangleRoundedFilledShapeVariant())
    {
    }
}
