using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Plans
{
    public class PlanLayout : IExposable
    {
        List<PlanCell> _cells = new();

        public IEnumerable<PlanCell> Cells => _cells;

        public int CellCount => _cells.Count;

        public AreaDimensions Dimensions { get; private set; }

        public PlanLayout()
        {
        }

        public PlanLayout(List<PlanCell> cells)
        {
            _cells.AddRange(cells);

            DetermineAreaDimensions();
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

        public void ExposeData()
        {
            Scribe_Collections.Look(ref _cells, "cells", LookMode.Deep);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
                DetermineAreaDimensions();
        }
        
        void DetermineAreaDimensions()
        {
            Dimensions = CellUtilities.DetermineAreaDimensions(_cells.Select(c => c.Position.ToIntVec3)); new AreaDimensions();
        }
    }
}
