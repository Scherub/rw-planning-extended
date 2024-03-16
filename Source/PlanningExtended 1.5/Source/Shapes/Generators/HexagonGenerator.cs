using System.Collections.Generic;
using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class HexagonGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _lineIndices = new()
        {
            new LineIndex(0, 1),
            new LineIndex(0, 2),
            new LineIndex(3, 4),
            new LineIndex(3, 5),
            new LineIndex(1, 4),
            new LineIndex(2, 5)
        };

        protected override List<LineIndex> LineIndices => _lineIndices;

        public HexagonGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            return rotation switch
            {
                Direction.Vertical => GetVerticesDirectionVertical(areaDimensions),
                _ => GetVerticesDirectionHorizontal(areaDimensions),
            };
        }

        List<IntVec3> GetVerticesDirectionHorizontal(AreaDimensions areaDimensions)
        {
            int quarterWidth = Mathf.RoundToInt(areaDimensions.Width / 4f);

            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MinX + quarterWidth, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX + quarterWidth, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MaxX - quarterWidth, 0, areaDimensions.MinZ),
                new(areaDimensions.MaxX - quarterWidth, 0, areaDimensions.MaxZ)
            };
        }

        List<IntVec3> GetVerticesDirectionVertical(AreaDimensions areaDimensions)
        {
            int quarterHeight = Mathf.RoundToInt(areaDimensions.Height / 4f);

            return new List<IntVec3>
            {
                new(areaDimensions.CenterX, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX, 0, areaDimensions.MinZ + quarterHeight),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ + quarterHeight),
                new(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ - quarterHeight),
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ - quarterHeight)
            };
        }
    }
}
