using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class EllipseGenerator : BaseShapeGenerator
    {
        bool DrawOutline { get; }

        bool DrawInnerArea { get; }

        public EllipseGenerator(bool drawOutline, bool drawInnerArea)
        {
            DrawOutline = drawOutline;
            DrawInnerArea = drawInnerArea;
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            
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

            return false;
        }
    }
}
