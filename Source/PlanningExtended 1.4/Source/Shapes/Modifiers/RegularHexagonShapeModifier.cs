using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class RegularHexagonShapeModifier : BasePolygonShapeModifier
    {
        protected override IntVec3 DetermineNewSize(AreaDimensions areaDimensions, Direction rotation)
        {
            return rotation switch
            {
                Direction.Vertical => DetermineNewSize(areaDimensions.Height, areaDimensions.Width).SwitchAxis(),
                _ => DetermineNewSize(areaDimensions.Width, areaDimensions.Height)
            };
        }

        static IntVec3 DetermineNewSize(int width, int height)
        {
            if (width < height)
            {
                return new IntVec3(width - 1, 0, GetHeightOfHexagon(width) - 1);
            }
            else
            {
                int newWidth = GetWidthOfHexagon(height);

                return newWidth > width ? new IntVec3(width - 1, 0, GetHeightOfHexagon(width) - 1) : new IntVec3(newWidth - 1, 0, height - 1);
            }
        }

        static int GetHeightOfHexagon(float width)
        {
            return Mathf.RoundToInt(TriangleUtilities.HeightOfEquilateralTriangle(width / 2f) * 2f);
        }

        static int GetWidthOfHexagon(int height)
        {
            return Mathf.RoundToInt(TriangleUtilities.SideLengthOfEquilateralTriangle(height / 2f) * 2f);
        }
    }
}
