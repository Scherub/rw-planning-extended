using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;

namespace PlanningExtended.Plans
{
    public class PlanLayout
    {
        readonly List<PlanCell> _cells = new();

        public IEnumerable<PlanCell> Cells => _cells;

        public int CellCount => _cells.Count;

        public AreaDimensions Dimensions { get; private set; }

        public PlanLayout(List<PlanCell> cells)
        {
            _cells.AddRange(cells);

            Dimensions = CellUtilities.DetermineAreaDimensions(cells.Select(c => c.Position.ToIntVec3)); new AreaDimensions();
        }

        public PlanLayout(List<PlanCell> cells, AreaDimensions areaDimensions)
        {
            _cells.AddRange(cells);

            Dimensions = areaDimensions;
        }

        public override string ToString()
        {
            return $"{CellCount} Cells {Dimensions}";
        }
    }
}
