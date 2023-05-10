using System.Collections.Generic;
using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class PentagonGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _lineIndices = new()
        {
            new LineIndex(0, 1),
            new LineIndex(0, 2),
            new LineIndex(1, 3),
            new LineIndex(2, 4),
            new LineIndex(3, 4)
        };

        protected override List<LineIndex> LineIndices => _lineIndices;

        public PentagonGenerator(bool fillArea)
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
            int oneFifthX = Mathf.RoundToInt(areaDimensions.Width / 5f);
            int twoFifthZ = Mathf.RoundToInt(areaDimensions.Height / 5f * 2f);

            return new List<IntVec3>
            {
                new(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ - twoFifthZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ - twoFifthZ),
                new(areaDimensions.MinX + oneFifthX, 0, areaDimensions.MinZ),
                new(areaDimensions.MaxX - oneFifthX, 0, areaDimensions.MinZ)
            };
        }

        List<IntVec3> GetVerticesDirectionSouth(AreaDimensions areaDimensions)
        {
            int oneFifthX = Mathf.RoundToInt(areaDimensions.Width / 5f);
            int twoFifthZ = Mathf.RoundToInt(areaDimensions.Height / 5f * 2f);

            return new List<IntVec3>
            {
                new(areaDimensions.CenterX, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX, 0, areaDimensions.MinZ + twoFifthZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ + twoFifthZ),
                new(areaDimensions.MinX + oneFifthX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX - oneFifthX, 0, areaDimensions.MaxZ)
            };
        }

        List<IntVec3> GetVerticesDirectionEast(AreaDimensions areaDimensions)
        {
            int oneFifthZ = Mathf.RoundToInt(areaDimensions.Height / 5f);
            int twoFifthX = Mathf.RoundToInt(areaDimensions.Width / 5f * 2f);

            return new List<IntVec3>
            {
                new(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MaxX - twoFifthX, 0, areaDimensions.MinZ),
                new(areaDimensions.MaxX - twoFifthX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MinX, 0, areaDimensions.MinZ + oneFifthZ),
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ - oneFifthZ),
            };
        }

        List<IntVec3> GetVerticesDirectionWest(AreaDimensions areaDimensions)
        {
            int oneFifthZ = Mathf.RoundToInt(areaDimensions.Height / 5f);
            int twoFifthX = Mathf.RoundToInt(areaDimensions.Width / 5f * 2f);

            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                new(areaDimensions.MinX + twoFifthX, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX + twoFifthX, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ + oneFifthZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ - oneFifthZ)
            };
        }
    }
}
