using System.Collections.Generic;
using PlanningExtended.Cells;
using UnityEngine;

namespace PlanningExtended.Shapes.Generators
{
    internal abstract class BaseShapeSegmentGenerator : BaseShapeGenerator
    {
        int _numberOfSegmentsX = 1;

        int _numberOfSegmentsZ = 1;

        HashSet<int> _segmentsX = new();

        HashSet<int> _segmentsZ = new();

        protected HashSet<int> SegmentsX => _segmentsX;

        protected HashSet<int> SegmentsZ => _segmentsZ;

        public void UpdateNumberOfSegments(int numberOfSegmentsX, int numberOfSegmentsZ)
        {
            if (numberOfSegmentsX != _numberOfSegmentsX)
            {
                _numberOfSegmentsX = numberOfSegmentsX;
                RequiresUpdate();
            }

            if (numberOfSegmentsZ != _numberOfSegmentsZ)
            {
                _numberOfSegmentsZ = numberOfSegmentsZ;
                RequiresUpdate();
            }
        }

        protected override void OnPreUpdate(AreaDimensions areaDimensions)
        {
            base.OnPreUpdate(areaDimensions);

            CalculateSegments(ref _segmentsX, _numberOfSegmentsX, areaDimensions.MinX, areaDimensions.MaxX);
            CalculateSegments(ref _segmentsZ, _numberOfSegmentsZ, areaDimensions.MinZ, areaDimensions.MaxZ);
        }

        void CalculateSegments(ref HashSet<int> segments, int numberOfSegments, int min, int max)
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
