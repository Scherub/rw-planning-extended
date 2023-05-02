using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class PointGridGenerator : BaseShapeGenerator
    {
        HashSet<int> _segmentsX = new();

        HashSet<int> _segmentsZ = new();

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
                    if (_segmentsX.Contains(x) && _segmentsZ.Contains(z))
                        yield return new IntVec3(x, 0, z);
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
