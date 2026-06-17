using System;
using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Modifiers.Dimensions;
using Verse;

namespace PlanningExtended.Shapes.Variants.Fill;

internal abstract class BaseFloodFillShapeVariant : BaseShapeVariant
{
    const int FloodFillMaxCells = 10_000;

    protected BaseFloodFillShapeVariant()
        : base(new NullShapeModifier())
    {
    }

    public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions) => true;

    public abstract HashSet<IntVec3> ComputeFillCells(Map map, IntVec3 startCell);

    protected HashSet<IntVec3> FloodFill(Map map, IntVec3 startCell, Func<IntVec3, bool> isCellValid)
    {
        HashSet<IntVec3> visited = [];
        HashSet<IntVec3> filled = [];
        Queue<IntVec3> queue = [];

        visited.Add(startCell);
        queue.Enqueue(startCell);

        while (queue.Count > 0)
        {
            IntVec3 current = queue.Dequeue();

            if (!isCellValid(current))
                continue;

            filled.Add(current);

            if (filled.Count > FloodFillMaxCells)
                return [];

            foreach (IntVec3 neighbor in current.CardinalNeighbors())
            {
                if (!visited.Contains(neighbor) && neighbor.InBounds(map) && !neighbor.InNoBuildEdgeArea(map))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return filled;
    }
}
