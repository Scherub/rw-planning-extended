using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class EllipseGenerator : BaseShapeGenerator
    {
        bool FillArea { get; }

        public EllipseGenerator(bool fillArea)
            : base()
        {
            FillArea = fillArea;
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            // generate code for ellipse outline
            // https://stackoverflow.com/questions/217578/how-can-i-draw-an-oval-in-html5-canvas
            
        }

        void DrawEllipseOutline(AreaDimensions areaDimensions)
        {
            int minX = areaDimensions.MinX;
            int minZ = areaDimensions.MinZ;
            int maxX = areaDimensions.MaxX;
            int maxZ = areaDimensions.MaxZ;
            int width = maxX - minX;
            int height = maxZ - minZ;
            DrawEllipse(minX, minZ, width, height);

            DrawEllipse(areaDimensions.MinX, areaDimensions.MinZ, areaDimensions.Width, areaDimensions.Height);
        }

        void DrawEllipse(int x, int y, int width, int height)
        {
            int radiusX = width / 2;
            int radiusY = height / 2;
            int centerX = x + radiusX;
            int centerY = y + radiusY;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int dx = i - radiusX;
                    int dy = j - radiusY;
                    if ((dx * dx * radiusY * radiusY + dy * dy * radiusX * radiusX) <= (radiusX * radiusX * radiusY * radiusY))
                    {
                        // inside ellipse
                    }
                }
            }
        }

        bool IsCellValid(AreaDimensions areaDimensions, int x, int z)
        {
            //if (DrawOutline)
            //{
            //    if (x == areaDimensions.MinX || x == areaDimensions.MaxX)
            //        return true;

            //    if (z == areaDimensions.MinZ || z == areaDimensions.MaxZ)
            //        return true;
            //}

            //if (DrawInnerArea)
            //{
            //    if (x > areaDimensions.MinX || x < areaDimensions.MaxX)
            //        return true;

            //    if (z > areaDimensions.MinZ || z < areaDimensions.MaxZ)
            //        return true;
            //}

            return false;
        }
    }
}
