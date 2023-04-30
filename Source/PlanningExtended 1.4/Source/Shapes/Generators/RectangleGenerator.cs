using System.Collections.Generic;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class RectangleGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _indices = new()
        {
            new LineIndex(0, 1),
            new LineIndex(1, 2),
            new LineIndex(2, 3),
            new LineIndex(3, 0)
        };

        protected override List<LineIndex> LineIndices => _indices;

        public RectangleGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MinZ),
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }
    }
}
