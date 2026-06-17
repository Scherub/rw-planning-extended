namespace PlanningExtended.Shapes.Variants.FillColor;

internal class FillColorAllShapeVariant : BaseFillColorShapeVariant
{
    public override bool MatchPlanType => false;

    public override ShapeVariant ShapeVariant => ShapeVariant.FillColorAll;
}
