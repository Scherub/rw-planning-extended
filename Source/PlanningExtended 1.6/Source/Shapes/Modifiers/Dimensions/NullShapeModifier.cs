using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions;

internal class NullShapeModifier : BaseShapeDimensionsModifier
{
    public override AreaDimensions OnUpdate(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation)
    {
        return areaDimensions;
    }
}
