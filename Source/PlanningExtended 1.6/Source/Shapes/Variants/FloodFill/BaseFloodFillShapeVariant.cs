using System;
using System.Collections.Generic;
using PlanningExtended.Shapes.Modifiers.Dimensions;
using Verse;

namespace PlanningExtended.Shapes.Variants.FloodFill;

internal abstract class BaseFloodFillShapeVariant : BaseShapeVariant
{
    const int FloodFillMaxCells = 50_000;

    protected BaseFloodFillShapeVariant()
        : base(new NullShapeModifier())
    {
    }

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
                return null;

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
