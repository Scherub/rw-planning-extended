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
            new LineIndex(2, 1),
            new LineIndex(2, 0)
        };

        protected override List<LineIndex> LineIndices => _lineIndices;

        public TriangleGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            return rotation switch
            {
                Direction.South => GetVerticesDirectionSouth(areaDimensions),
                Direction.East => GetVerticesDirectionEast(areaDimensions),
                Direction.West => GetVerticesDirectionWest(areaDimensions),
                _ => GetVerticesDirectionNorth(areaDimensions),
            };
        }

        List<IntVec3> GetVerticesDirectionNorth(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.MinZ),
                new(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }

        List<IntVec3> GetVerticesDirectionSouth(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                new(areaDimensions.CenterX, 0, areaDimensions.MinZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ)
            };
        }

        List<IntVec3> GetVerticesDirectionEast(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MinX, 0, areaDimensions.MinZ)
            };
        }

        List<IntVec3> GetVerticesDirectionWest(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }
    }
}
