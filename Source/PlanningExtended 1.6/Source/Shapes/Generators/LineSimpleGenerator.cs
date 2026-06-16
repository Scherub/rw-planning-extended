using System;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Plotter;
using Verse;

namespace PlanningExtended.Shapes.Generators;

internal class LineSimpleGenerator : BaseShapeGenerator
{
    bool _isPadded;

    public LineSimpleGenerator(bool isPadded)
        : base(false)
    {
        _isPadded = isPadded;
    }

    protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
    {
        IntVec3 endPosition = new(areaDimensions.Width == 1 ? areaDimensions.MinX : mousePosition.x, 0, areaDimensions.Height == 1 ? areaDimensions.MinZ : mousePosition.z);
        IntVec3 startPosition = areaDimensions.GetStartPosition(endPosition);

        // When modifier is active and it isn't a square, a horizontal or vertical line,
        // calculate the diagonal line that fits within the area dimensions .
        if (applyShapeDimensionsModifier && areaDimensions.Width != areaDimensions.Height && areaDimensions.Width > 1 && areaDimensions.Height > 1)
        {
            int dx = endPosition.x - startPosition.x;
            int dz = endPosition.z - startPosition.z;
            int extent = Math.Min(Math.Abs(dx), Math.Abs(dz));
            endPosition = new(startPosition.x + Math.Sign(dx) * extent, 0, startPosition.z + Math.Sign(dz) * extent);
        }

        AddValidCells(LinePlotter.PlotLine(startPosition, endPosition, _isPadded));
    }
}
