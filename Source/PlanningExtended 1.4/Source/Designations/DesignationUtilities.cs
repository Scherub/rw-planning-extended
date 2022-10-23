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
                PlanDesignitionType.PlanDoors => PlanningDesignationDefOf.PlanDoors,
                PlanDesignitionType.PlanDoorsColored => PlanningDesignationDefOf.PlanDoorsColored,
                PlanDesignitionType.PlanFloors => PlanningDesignationDefOf.PlanFloors,
                PlanDesignitionType.PlanFloorsColored => PlanningDesignationDefOf.PlanFloorsColored,
                PlanDesignitionType.PlanObjects => PlanningDesignationDefOf.PlanObjects,
                PlanDesignitionType.PlanObjectsColored => PlanningDesignationDefOf.PlanObjectsColored,
                PlanDesignitionType.PlanWall => PlanningDesignationDefOf.PlanWalls,
                PlanDesignitionType.PlanWallColored => PlanningDesignationDefOf.PlanWallsColored,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
