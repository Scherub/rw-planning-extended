using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class PointGridGenerator : BaseShapeSegmentGenerator
    {
        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
                for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                    if (SegmentsX.Contains(x) && SegmentsZ.Contains(z))
                        AddValidCell(x, z);
        }
    }
}
