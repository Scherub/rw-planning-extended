using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Plotter;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal abstract class BaseShapeGenerator
    {
        bool _requiresUpdate;

        AreaDimensions _areaDimensions;

        protected bool FillArea { get; }

        protected HashSet<IntVec3> ValidCells { get; } = new();

        protected BaseShapeGenerator(bool fillArea)
        {
            FillArea = fillArea;
        }

        public HashSet<IntVec3> Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            // TODO: implement equals
            if (!_requiresUpdate && areaDimensions == _areaDimensions && !shape.SelectedShapeVariant.ShapeFeatureManager.RequiresUpdate)
                return ValidCells;

            _areaDimensions = areaDimensions;

            if (shape.SelectedShapeVariant.ShapeFeatureManager.HasSegmentFeature)
                OnUpdateSegments(areaDimensions, shape.SelectedShapeVariant.ShapeFeatureManager.SegmentShapeFeature);

            ValidCells.Clear();

            OnUpdate(areaDimensions, mousePosition, rotation, applyShapeDimensionsModifier);

            _requiresUpdate = false;
            shape.SelectedShapeVariant.ShapeFeatureManager.HandledUpdates();

            return ValidCells;
        }

        public bool IsCellValid(IntVec3 cell)
        {
            return ValidCells.Contains(cell);
        }

        protected abstract void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier);

        protected virtual void OnUpdateSegments(AreaDimensions areaDimensions, SegmentShapeFeature segmentShapeFeature)
        {

        }

        protected void AddValidCells(IEnumerable<IntVec3> cells)
        {
            ValidCells.AddRange(cells);
        }

        protected void DrawAreaFilling(HashSet<IntVec3> cells)
        {
            foreach (IGrouping<int, IntVec3> row in cells.GroupBy(c => c.z))
            {
                var orderedRow = row.OrderBy(r => r.x);

                IntVec3 firstCell = row.First();
                IntVec3 lastCell = row.Last();

                AddValidCells(LinePlotter.PlotLineHorizontal(firstCell, lastCell));
            }
        }
    }
}
