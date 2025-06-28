using UnityEngine;

namespace PlanningExtended
{
    internal static class OctagonUtilities
    {
        static readonly float v = 1 + Mathf.Sqrt(2);

        public static float CalculateSideLength(float length)
        {
            return length / v;
        }
    }
}
