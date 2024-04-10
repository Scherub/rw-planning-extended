using UnityEngine;

namespace PlanningExtended
{
    internal static class TriangleUtilities
    {
        static readonly float sqrt3div2 = Mathf.Sqrt(3) / 2;

        static readonly float sqrt2 = Mathf.Sqrt(2);

        public static float HeightOfEquilateralTriangle(float sideLength)
        {
            return sqrt3div2 * sideLength;
        }

        public static float SideLengthOfEquilateralTriangle(float height)
        {
            return height / sqrt3div2;
        }

        public static float HeightOfRightTriangle(float catheusA, float catheusB)
        {
            return catheusA * catheusB / HypothenuseOfRightTriangle(catheusA, catheusB);
        }

        public static float HeightOfRightIsocelesTriangle(float catheus)
        {
            return catheus / sqrt2;
        }

        public static float HypothenuseOfRightTriangle(float catheusA, float catheusB)
        {
            return Mathf.Sqrt(catheusA * catheusA + catheusB * catheusB);
        }

        public static float HypothenuseOfRightIsocelesTriangle(float catheus)
        {
            return sqrt2 * catheus;
        }

        public static float CatheusOfRightIsocelesTriangle(float hypotenuse)
        {
            return hypotenuse / sqrt2;
        }
    }
}
