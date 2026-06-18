using System.Collections.Generic;
using Verse;

namespace PlanningExtended;

public static class IntVec3Extensions
{
    public static IntVec3 SwitchAxis(this IntVec3 position)
    {
        return new(position.z, position.y, position.x);
    }

    public static IEnumerable<IntVec3> CardinalNeighbors(this IntVec3 cell)
    {
        yield return new IntVec3(cell.x + 1, cell.y, cell.z);
        yield return new IntVec3(cell.x - 1, cell.y, cell.z);
        yield return new IntVec3(cell.x, cell.y, cell.z + 1);
        yield return new IntVec3(cell.x, cell.y, cell.z - 1);
    }
}
