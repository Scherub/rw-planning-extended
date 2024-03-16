using System;
using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Designations
{
    public static class PlanDesignationUtilities
    {
        public static DesignationDef GetDesignationDef(PlanDesignationType type)
        {
            return type switch
            {
                PlanDesignationType.PlanDoors => PlanningDesignationDefOf.PlanDoors,
                PlanDesignationType.PlanDoorsColored => PlanningDesignationDefOf.PlanDoorsColored,
                PlanDesignationType.PlanFloors => PlanningDesignationDefOf.PlanFloors,
                PlanDesignationType.PlanFloorsColored => PlanningDesignationDefOf.PlanFloorsColored,
                PlanDesignationType.PlanObjects => PlanningDesignationDefOf.PlanObjects,
                PlanDesignationType.PlanObjectsColored => PlanningDesignationDefOf.PlanObjectsColored,
                PlanDesignationType.PlanWall => PlanningDesignationDefOf.PlanWalls,
                PlanDesignationType.PlanWallColored => PlanningDesignationDefOf.PlanWallsColored,
                _ => throw new NotImplementedException(),
            };
        }

        public static IEnumerable<PlanDesignationType> GetPlanDesignationTypes()
        {
            yield return PlanDesignationType.PlanWall;
            yield return PlanDesignationType.PlanFloors;
            yield return PlanDesignationType.PlanDoors;
            yield return PlanDesignationType.PlanObjects;
        }
    }
}
