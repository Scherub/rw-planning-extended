using UnityEngine;

namespace PlanningExtended
{
    internal static class TriangleUtilities
    {
        static readonly float v = Mathf.Sqrt(3) / 2;

        public static float HeightOfEquilateralTriangle(float sideLength)
        {
            return v * sideLength;
        }

        public static float SideLengthOfEquilateralTriangle(float height)
        {
            return height / v;
        }
    }
}
