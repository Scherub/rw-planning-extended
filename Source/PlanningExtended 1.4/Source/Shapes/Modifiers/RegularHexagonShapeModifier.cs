using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class RegularHexagonShapeModifier : BasePolygonShapeModifier
    {
        protected override IntVec3 DetermineNewSize(AreaDimensions areaDimensions)
        {
            if (areaDimensions.Width < areaDimensions.Height)
            {
                return new IntVec3(areaDimensions.Width - 1, 0, GetHeightOfHexagon(areaDimensions.Width) - 1);
            }
            else
            {
                int width = GetWidthOfHexagon(areaDimensions.Height);

                return width > areaDimensions.Width ? new IntVec3(areaDimensions.Width - 1, 0, GetHeightOfHexagon(areaDimensions.Width) - 1) : new IntVec3(width - 1, 0, areaDimensions.Height - 1);
            }
        }

        static int GetHeightOfHexagon(float width)
        {
            return (int)Mathf.Round(TriangleUtilities.HeightOfEquilateralTriangle(width / 2f) * 2f);
        }

        static int GetWidthOfHexagon(int height)
        {
            return (int)Mathf.Round(TriangleUtilities.SideLengthOfEquilateralTriangle(height / 2f) * 2f);
        }
    }
}
