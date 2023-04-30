using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class EquilateralTriangleShapeModifier : BasePolygonShapeModifier
    {
        protected override IntVec3 DetermineNewSize(AreaDimensions areaDimensions)
        {
            if (areaDimensions.Width < areaDimensions.Height)
            {
                return new(areaDimensions.Width - 1, 0, GetHeightOfTriangle(areaDimensions.Width) - 1);
            }
            else
            {
                int width = GetSideLengthOfTriangle(areaDimensions.Height);

                return width > areaDimensions.Width ? new IntVec3(areaDimensions.Width - 1, 0, GetHeightOfTriangle(areaDimensions.Width) - 1) : new IntVec3(width - 1, 0, areaDimensions.Height - 1);
            }
        }

        static int GetHeightOfTriangle(int width)
        {
            return (int)Mathf.Round(TriangleUtilities.HeightOfEquilateralTriangle(width));
        }

        static int GetSideLengthOfTriangle(int height)
        {
            return (int)Mathf.Round(TriangleUtilities.SideLengthOfEquilateralTriangle(height));
        }
    }
}
