using Verse;

namespace PlanningExtended.Defs
{
    internal static class DesignationDefUtilities
    {
        public static PlanDesignationType GetType(DesignationDef designationDef)
        {
            if (designationDef == PlanningDesignationDefOf.PlanDoors || designationDef == PlanningDesignationDefOf.PlanDoorsColored)
                return PlanDesignationType.PlanDoors;
            else if (designationDef == PlanningDesignationDefOf.PlanFloors || designationDef == PlanningDesignationDefOf.PlanFloorsColored)
                return PlanDesignationType.PlanFloors;
            else if (designationDef == PlanningDesignationDefOf.PlanObjects || designationDef == PlanningDesignationDefOf.PlanObjectsColored)
                return PlanDesignationType.PlanObjects;
            else if (designationDef == PlanningDesignationDefOf.PlanWalls || designationDef == PlanningDesignationDefOf.PlanWallsColored)
                return PlanDesignationType.PlanWall;

            return PlanDesignationType.Unknown;
        }
    }
}
