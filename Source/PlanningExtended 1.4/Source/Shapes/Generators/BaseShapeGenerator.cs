using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal abstract class BaseShapeGenerator
    {
        bool _requiresUpdate;

        AreaDimensions _areaDimensions;

        readonly HashSet<IntVec3> _validCells = new();

        public HashSet<IntVec3> Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            // TODO: implement equals
            if (!_requiresUpdate && areaDimensions == _areaDimensions && !shape.SelectedShapeVariant.ShapeFeatureManager.RequiresUpdate)
                return _validCells;

            _areaDimensions = areaDimensions;

            if (shape.SelectedShapeVariant.ShapeFeatureManager.HasSegmentFeature)
                OnUpdateSegments(areaDimensions, shape.SelectedShapeVariant.ShapeFeatureManager.SegmentShapeFeature);

            _validCells.Clear();

            OnUpdate(areaDimensions, mousePosition, rotation, applyShapeDimensionsModifier);

            _requiresUpdate = false;
            shape.SelectedShapeVariant.ShapeFeatureManager.HandledUpdates();

            return _validCells;
        }

        public bool IsCellValid(IntVec3 cell)
        {
            return _validCells.Contains(cell);
        }

        protected abstract void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier);

        protected virtual void OnUpdateSegments(AreaDimensions areaDimensions, SegmentShapeFeature segmentShapeFeature)
        {

        }

        protected void AddValidCells(IEnumerable<IntVec3> cells)
        {
            _validCells.AddRange(cells);
        }
    }
}
