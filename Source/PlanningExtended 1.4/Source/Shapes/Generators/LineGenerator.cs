using System;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class LineGenerator : BaseShapeGenerator
    {
        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            // derivation of Bresenham's line algorithm: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm

            IntVec3 endPosition = new(areaDimensions.Width == 0 ? areaDimensions.MinX : mousePosition.x, 0, areaDimensions.Height == 0 ? areaDimensions.MinZ : mousePosition.z);
            IntVec3 startPosition = areaDimensions.GetStartPosition(endPosition);

            int deltaX = endPosition.x - startPosition.x;
            int deltaZ = endPosition.z - startPosition.z;

            int deltaAbsX = Math.Abs(deltaX);
            int deltaAbsZ = Math.Abs(deltaZ);

            if (deltaAbsX >= deltaAbsZ)
            {
                int err = 2 * deltaAbsZ - deltaAbsX;

                int x = deltaX >= 0 ? startPosition.x : endPosition.x;
                int z = deltaX >= 0 ? startPosition.z : endPosition.z;
                int endX = deltaX >= 0 ? endPosition.x : startPosition.x;
                int stepZ = deltaX < 0 && deltaZ < 0 || deltaX > 0 && deltaZ > 0 ? 1 : -1;

                for (; x <= endX; x++)
                {
                    AddValidCell(x, z);

                    if (err < 0)
                    {
                        err += 2 * deltaAbsZ;
                    }
                    else
                    {
                        z += stepZ;
                        err += 2 * (deltaAbsZ - deltaAbsX);
                    }
                }
            }
            else
            {
                int err = 2 * deltaAbsX - deltaAbsZ;

                int x = deltaZ >= 0 ? startPosition.x : endPosition.x;
                int z = deltaZ >= 0 ? startPosition.z : endPosition.z;
                int endZ = deltaZ >= 0 ? endPosition.z : startPosition.z;
                int stepX = deltaX < 0 && deltaZ < 0 || deltaX > 0 && deltaZ > 0 ? 1 : -1;

                for (; z <= endZ; z++)
                {
                    AddValidCell(x, z);

                    if (err < 0)
                    {
                        err += 2 * deltaAbsX;
                    }
                    else
                    {
                        x += stepX;
                        err += 2 * (deltaAbsX - deltaAbsZ);
                    }
                }
            }
        }
    }
}