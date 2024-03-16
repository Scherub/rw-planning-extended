using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class OldRectangleGenerator : BaseShapeGenerator
    {
        HashSet<int> _segmentsX = new();

        HashSet<int> _segmentsZ = new();

        bool DrawOutline { get; }

        bool DrawInnerArea { get; }

        bool DrawGrid { get; }

        bool DrawIntersectionPoints { get; }

        public OldRectangleGenerator(bool drawOutline, bool drawInnerArea, bool drawGrid, bool drawIntersectionPoints)
            : base(false)
        {
            DrawOutline = drawOutline;
            DrawInnerArea = drawInnerArea;
            DrawGrid = drawGrid;
            DrawIntersectionPoints = drawIntersectionPoints;
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            AddValidCells(GetVertices(areaDimensions));
        }

        protected override void OnUpdateSegments(AreaDimensions areaDimensions, SegmentShapeFeature segmentShapeFeature)
        {
            CalculateSegments(areaDimensions.MinX, areaDimensions.MaxX, ref _segmentsX, segmentShapeFeature.NumberOfSegmentsX);
            CalculateSegments(areaDimensions.MinZ, areaDimensions.MaxZ, ref _segmentsZ, segmentShapeFeature.NumberOfSegmentsZ);

            segmentShapeFeature.HandledUpdate();
        }

        IEnumerable<IntVec3> GetVertices(AreaDimensions areaDimensions)
        {
            for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
                for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                    if (IsCellValid(areaDimensions, x, z))
                        yield return new IntVec3(x, 0, z);
        }

        bool IsCellValid(AreaDimensions areaDimensions, int x, int z)
        {
            if (DrawOutline)
            {
                if (x == areaDimensions.MinX || x == areaDimensions.MaxX)
                    return true;

                if (z == areaDimensions.MinZ || z == areaDimensions.MaxZ)
                    return true;
            }

            if (DrawInnerArea)
            {
                if (x > areaDimensions.MinX || x < areaDimensions.MaxX)
                    return true;

                if (z > areaDimensions.MinZ || z < areaDimensions.MaxZ)
                    return true;
            }

            if (DrawGrid)
            {
                if (_segmentsX.Contains(x))
                    return true;

                if (_segmentsZ.Contains(z))
                    return true;
            }

            if (DrawIntersectionPoints)
            {
                if (_segmentsX.Contains(x) && _segmentsZ.Contains(z))
                    return true;
            }

            return false;
        }

        void CalculateSegments(int min, int max, ref HashSet<int> segments, int numberOfSegments)
        {
            segments.Clear();

            int length = max - min;

            for (int i = 1; i <= numberOfSegments; i++)
            {
                int segment = Mathf.RoundToInt(i / (float)(numberOfSegments + 1) * length);

                segments.Add(min + segment);
            }
        }
    }
}
