using System.Collections.Generic;
using PlanningExtended.Cells;
using UnityEngine;
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

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            if (FillArea && rotation == Direction.Horizontal)
                DrawFilledRectangle(areaDimensions);
            else
                base.OnUpdate(areaDimensions, mousePosition, rotation, applyShapeDimensionsModifier);
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            return rotation switch
            {
                Direction.Diagonal => GetVerticesDirectionDiagonal(areaDimensions),
                Direction.DiagonalNW => GetVerticesDirectionDiagonalNW(areaDimensions),
                Direction.DiagonalNE => GetVerticesDirectionDiagonal(areaDimensions),
                _ => GetVerticesDirectionHorizontal(areaDimensions),
            };
        }

        List<IntVec3> GetVerticesDirectionHorizontal(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MinZ),
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }

        List<IntVec3> GetVerticesDirectionDiagonal(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MinZ)
            };
        }

        List<IntVec3> GetVerticesDirectionDiagonalNW(AreaDimensions areaDimensions)
        {
            int minSide = Mathf.Min(areaDimensions.Width, areaDimensions.Height);
            int maxSide = Mathf.Max(areaDimensions.Width, areaDimensions.Height);

            float rectangleWidth = TriangleUtilities.HypothenuseOfRightIsocelesTriangle(minSide);

            int restSide = maxSide - minSide;

            int halfRestSide = Mathf.RoundToInt(restSide / 2f);

            float rectangleHeight = TriangleUtilities.CatheusOfRightIsocelesTriangle(restSide);

            int marginX = Mathf.RoundToInt(TriangleUtilities.HeightOfRightIsocelesTriangle(rectangleHeight));

            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MaxZ - restSide),
                new IntVec3(areaDimensions.MinX + marginX, 0, areaDimensions.MaxZ - halfRestSide),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MinZ + restSide),
                new IntVec3(areaDimensions.MaxX - marginX, 0, areaDimensions.MinZ + halfRestSide)
            };
        }

        void DrawFilledRectangle(AreaDimensions areaDimensions)
        {
            HashSet<IntVec3> vertices = new();

            for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
                for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                    vertices.Add(new IntVec3(x, 0, z));

            AddValidCells(vertices);
        }
    }
}
