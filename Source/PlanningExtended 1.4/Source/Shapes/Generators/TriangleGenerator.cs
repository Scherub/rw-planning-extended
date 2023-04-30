using System.Collections.Generic;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class TriangleGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _lineIndices = new()
        {
            new LineIndex(0, 1),
            new LineIndex(1, 2),
            new LineIndex(2, 0)
        };

        protected override List<LineIndex> LineIndices => _lineIndices;

        public TriangleGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.MinZ),
                new(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }
    }
}
