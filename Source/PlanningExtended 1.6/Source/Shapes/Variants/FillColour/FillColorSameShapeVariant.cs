namespace PlanningExtended.Shapes.Variants.FillColor;

internal class FillColorSameShapeVariant : BaseFillColorShapeVariant
{
    public override bool MatchPlanType => true;

    public override ShapeVariant ShapeVariant => ShapeVariant.FillColorSame;
}
