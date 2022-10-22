using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Cells
{
    public class CellArea
    {
        readonly List<IntVec3> _cells = new();

        public IEnumerable<IntVec3> Cells => _cells;

        public int CellCount => _cells.Count;

        public bool IsEmpty => CellCount == 0;

        public AreaDimensions Dimensions { get; private set; }

        public CellArea(IEnumerable<IntVec3> cells)
        {
            _cells.AddRange(cells);

            Dimensions = CellUtilities.DetermineAreaDimensions(cells);
        }

        public override string ToString()
        {
            return $"{Dimensions}";
        }
    }
}
