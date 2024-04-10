using PlanningExtended.Plans;
using Verse;

namespace PlanningExtended
{
    public static class PlanLayoutExtensions
    {
        public static IntVec2 FindMostBottomRightCell(this PlanLayout planLayout)
        {
            int maxX = planLayout.Dimensions.MaxX;
            int x = 0;
            int z = planLayout.Dimensions.MaxZ;

            foreach (var planCell in planLayout.Cells)
            {
                if (planCell.Position.z == 0 && planCell.Position.x > x)
                    x = planCell.Position.x;

                if (planCell.Position.x == maxX && planCell.Position.z < z)
                    z = planCell.Position.z;
            }

            return x < z ? new(x, 0) : new(maxX, z);
        }
    }
}
