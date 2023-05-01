using System.Collections.Generic;
using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class OctagonGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _lineIndices = new() {
            new LineIndex(0, 1),
            new LineIndex(1, 2),
            new LineIndex(2, 3),
            new LineIndex(3, 4),
            new LineIndex(4, 5),
            new LineIndex(5, 6),
            new LineIndex(6, 7),
            new LineIndex(7, 0)
        };

        protected override List<LineIndex> LineIndices => _lineIndices;

        public OctagonGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            float horizontalSideLength = OctagonUtilities.CalculateSideLength(areaDimensions.Width);
            float verticalSideLength = OctagonUtilities.CalculateSideLength(areaDimensions.Height);

            int partialWidth = Mathf.RoundToInt((areaDimensions.Width - horizontalSideLength) / 2f);
            int partialHeight = Mathf.RoundToInt((areaDimensions.Height - verticalSideLength) / 2f);

            //Log.Warning($"AD: {areaDimensions}, W: {areaDimensions.Width}, H: {areaDimensions.Height}, HSL: {horizontalSideLength}, VSL: {verticalSideLength}, PW: {partialWidth}, PH: {partialHeight}, V1: {new IntVec3(areaDimensions.MinX + partialWidth, 0, areaDimensions.MaxZ)}, V2: {new IntVec3(areaDimensions.MaxX - partialWidth, 0, areaDimensions.MaxZ)}");

            return new List<IntVec3>
            {
                new(areaDimensions.MinX, 0, areaDimensions.MaxZ - partialHeight),
                new(areaDimensions.MinX + partialWidth, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX - partialWidth, 0, areaDimensions.MaxZ),
                new(areaDimensions.MaxX, 0, areaDimensions.MaxZ - partialHeight),
                new(areaDimensions.MaxX, 0, areaDimensions.MinZ + partialHeight),
                new(areaDimensions.MaxX - partialWidth, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX + partialWidth, 0, areaDimensions.MinZ),
                new(areaDimensions.MinX, 0, areaDimensions.MinZ + partialHeight),
            };
        }
    }
}
