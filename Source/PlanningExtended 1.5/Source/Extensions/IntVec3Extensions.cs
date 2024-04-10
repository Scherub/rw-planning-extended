using Verse;

namespace PlanningExtended
{
    public static class IntVec3Extensions
    {
        public static IntVec3 SwitchAxis(this IntVec3 position)
        {
            return new(position.z, position.y, position.x);
        }

    }
}
