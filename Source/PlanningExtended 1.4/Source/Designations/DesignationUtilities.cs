using System;
using Verse;

namespace PlanningExtended.Designations
{
    public static class DesignationUtilities
    {
        public static DesignationDef GetDesignationDef(PlanDesignitionType type)
        {
            return type switch
            {
                PlanDesignitionType.PlanDoors => DesignationDefOf.PlanDoors,
                PlanDesignitionType.PlanDoorsColored => DesignationDefOf.PlanDoorsColored,
                PlanDesignitionType.PlanFloors => DesignationDefOf.PlanFloors,
                PlanDesignitionType.PlanFloorsColored => DesignationDefOf.PlanFloorsColored,
                PlanDesignitionType.PlanObjects => DesignationDefOf.PlanObjects,
                PlanDesignitionType.PlanObjectsColored => DesignationDefOf.PlanObjectsColored,
                PlanDesignitionType.PlanWall => DesignationDefOf.PlanWalls,
                PlanDesignitionType.PlanWallColored => DesignationDefOf.PlanWallsColored,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
