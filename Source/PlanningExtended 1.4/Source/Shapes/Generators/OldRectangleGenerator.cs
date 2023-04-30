using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class OldRectangleGenerator : BaseShapeSegmentGenerator
    {
        bool DrawOutline { get; }

        bool DrawInnerArea { get; }

        bool DrawGrid { get; }

        bool DrawIntersectionPoints { get; }

        public OldRectangleGenerator(bool drawOutline, bool drawInnerArea, bool drawGrid, bool drawIntersectionPoints)
        {
            DrawOutline = drawOutline;
            DrawInnerArea = drawInnerArea;
            DrawGrid = drawGrid;
            DrawIntersectionPoints = drawIntersectionPoints;
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
                for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                    if (IsCellValid(areaDimensions, x, z))
                        AddValidCell(x, z);
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
                if (SegmentsX.Contains(x))
                    return true;

                if (SegmentsZ.Contains(z))
                    return true;
            }

            if (DrawIntersectionPoints)
            {
                if (SegmentsX.Contains(x) && SegmentsZ.Contains(z))
                    return true;
            }

            return false;
        }
    }
}
