using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions;

internal class LineSimpleShapeModifier : BaseShapeDimensionsModifier
{
    // Equal 22.5° snap zones: snap to diagonal when max/min dimension ratio is below sqrt(2)+1
    const float DiagonalThreshold = 2.414f;

    public override AreaDimensions OnUpdate(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation)
    {
        IntVec3 startPosition = areaDimensions.GetStartPosition(mousePosition);

        if (areaDimensions.Width >= areaDimensions.Height * DiagonalThreshold)
            return new AreaDimensions(areaDimensions.MinX, startPosition.z, areaDimensions.MaxX, startPosition.z);
        else if (areaDimensions.Height >= areaDimensions.Width * DiagonalThreshold)
            return new AreaDimensions(startPosition.x, areaDimensions.MinZ, startPosition.x, areaDimensions.MaxZ);

        return areaDimensions;
    }
}
