using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Plotter
{
    internal static class LinePlotter
    {
        public static IEnumerable<IntVec3> PlotLine(IntVec3 startPosition, IntVec3 endPosition)
        {
            if (startPosition.x == endPosition.x)
                return PlotLineVertical(startPosition, endPosition);
            else if (startPosition.z == endPosition.z)
                return PlotLineHorizontal(startPosition, endPosition);
            else
                return PlotLineAny(startPosition, endPosition);
        }

        public static IEnumerable<IntVec3> PlotLineHorizontal(IntVec3 startPosition, IntVec3 endPosition)
        {
            if (startPosition.x > endPosition.x)
                (startPosition, endPosition) = (endPosition, startPosition);

            for (int x = startPosition.x; x <= endPosition.x; x++)
                yield return new IntVec3(x, 0, startPosition.z);
        }
        
        public static IEnumerable<IntVec3> PlotLineVertical(IntVec3 startPosition, IntVec3 endPosition)
        {
            if (startPosition.z > endPosition.z)
                (startPosition, endPosition) = (endPosition, startPosition);

            for (int z = startPosition.z; z <= endPosition.z; z++)
                yield return new IntVec3(startPosition.x, 0, z);
        }

        public static IEnumerable<IntVec3> PlotLineAny(IntVec3 startPosition, IntVec3 endPosition)
        {
            // derivation of Bresenham's line algorithm: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm

            int deltaX = endPosition.x - startPosition.x;
            int deltaZ = endPosition.z - startPosition.z;

            int deltaAbsX = Mathf.Abs(deltaX);
            int deltaAbsZ = Mathf.Abs(deltaZ);

            if (deltaAbsX >= deltaAbsZ)
            {
                int err = 2 * deltaAbsZ - deltaAbsX;

                int x = deltaX >= 0 ? startPosition.x : endPosition.x;
                int z = deltaX >= 0 ? startPosition.z : endPosition.z;
                int endX = deltaX >= 0 ? endPosition.x : startPosition.x;
                int stepZ = deltaX < 0 && deltaZ < 0 || deltaX > 0 && deltaZ > 0 ? 1 : -1;

                for (; x <= endX; x++)
                {
                    yield return new IntVec3(x, 0, z);

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
                    yield return new IntVec3(x, 0, z);

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
