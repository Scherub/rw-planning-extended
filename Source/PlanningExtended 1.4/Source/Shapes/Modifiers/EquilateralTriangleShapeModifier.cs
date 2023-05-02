using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class EquilateralTriangleShapeModifier : BasePolygonShapeModifier
    {
        protected override IntVec3 DetermineNewSize(AreaDimensions areaDimensions, Direction rotation)
        {
            return rotation switch
            {
                Direction.East or Direction.West => DetermineNewSize(areaDimensions.Height, areaDimensions.Width).SwitchAxis(),
                _ => DetermineNewSize(areaDimensions.Width, areaDimensions.Height)
            };
        }

        static IntVec3 DetermineNewSize(int width, int height)
        {
            if (width < height)
            {
                return new(width - 1, 0, GetHeightOfTriangle(width) - 1);
            }
            else
            {
                int newWidth = GetSideLengthOfTriangle(height);

                return newWidth > width ? new IntVec3(width - 1, 0, GetHeightOfTriangle(width) - 1) : new IntVec3(newWidth - 1, 0, height - 1);
            }
        }

        static int GetHeightOfTriangle(int width)
        {
            return Mathf.RoundToInt(TriangleUtilities.HeightOfEquilateralTriangle(width));
        }

        static int GetSideLengthOfTriangle(int height)
        {
            return Mathf.RoundToInt(TriangleUtilities.SideLengthOfEquilateralTriangle(height));
        }
    }
}
