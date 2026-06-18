using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Plotter;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators;

internal class RectangleRoundedGenerator : BaseShapeGenerator
{
    public RectangleRoundedGenerator(bool fillArea)
        : base(fillArea)
    {
    }

    protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
    {
        int radius = Mathf.Max(1, Mathf.Min(areaDimensions.Width, areaDimensions.Height) / 4);

        int innerMinX = areaDimensions.MinX + radius;
        int innerMaxX = areaDimensions.MaxX - radius;
        int innerMinZ = areaDimensions.MinZ + radius;
        int innerMaxZ = areaDimensions.MaxZ - radius;

        // Straight edges
        AddValidCells(LinePlotter.PlotLineHorizontal(new IntVec3(innerMinX, 0, areaDimensions.MaxZ), new IntVec3(innerMaxX, 0, areaDimensions.MaxZ)));
        AddValidCells(LinePlotter.PlotLineHorizontal(new IntVec3(innerMinX, 0, areaDimensions.MinZ), new IntVec3(innerMaxX, 0, areaDimensions.MinZ)));
        AddValidCells(LinePlotter.PlotLineVertical(new IntVec3(areaDimensions.MinX, 0, innerMinZ), new IntVec3(areaDimensions.MinX, 0, innerMaxZ)));
        AddValidCells(LinePlotter.PlotLineVertical(new IntVec3(areaDimensions.MaxX, 0, innerMinZ), new IntVec3(areaDimensions.MaxX, 0, innerMaxZ)));

        // Corner arcs
        AddValidCells(PlotCornerArcs(innerMinX, innerMaxX, innerMinZ, innerMaxZ, radius));

        if (FillArea)
            DrawAreaFilling(ValidCells);
    }

    IEnumerable<IntVec3> PlotCornerArcs(int innerMinX, int innerMaxX, int innerMinZ, int innerMaxZ, int radius)
    {
        int x = 0;
        int z = radius;
        int d = 3 - 2 * radius;

        while (x <= z)
        {
            // Bottom-left corner center: (innerMinX, innerMinZ)
            yield return new IntVec3(innerMinX - x, 0, innerMinZ - z);
            yield return new IntVec3(innerMinX - z, 0, innerMinZ - x);

            // Bottom-right corner center: (innerMaxX, innerMinZ)
            yield return new IntVec3(innerMaxX + x, 0, innerMinZ - z);
            yield return new IntVec3(innerMaxX + z, 0, innerMinZ - x);

            // Top-left corner center: (innerMinX, innerMaxZ)
            yield return new IntVec3(innerMinX - x, 0, innerMaxZ + z);
            yield return new IntVec3(innerMinX - z, 0, innerMaxZ + x);

            // Top-right corner center: (innerMaxX, innerMaxZ)
            yield return new IntVec3(innerMaxX + x, 0, innerMaxZ + z);
            yield return new IntVec3(innerMaxX + z, 0, innerMaxZ + x);

            if (d < 0)
                d += 4 * x + 6;
            else
            {
                d += 4 * (x - z) + 10;
                z--;
            }

            x++;
        }
    }
}
