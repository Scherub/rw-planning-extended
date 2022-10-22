using System.Collections.Generic;
using RimWorld;
using Verse;
using RimWorldDesignationDefOf = RimWorld.DesignationDefOf;

namespace PlanningExtended
{
    [DefOf]
    public static class DesignationDefOf
    {
        static List<DesignationDef> _designationDefs;
        public static List<DesignationDef> DesignationDefs
        {
            get
            {
                _designationDefs ??= new(new[]
                {
                    RimWorldDesignationDefOf.Plan,
                    PlanDoors,
                    PlanDoorsColored,
                    PlanFloors,
                    PlanFloorsColored,
                    PlanObjects,
                    PlanObjectsColored,
                    PlanWalls,
                    PlanWallsColored
                });
                
                return _designationDefs;
            }
        }

        public static DesignationDef PlanDoors;
        
        public static DesignationDef PlanDoorsColored;

        public static DesignationDef PlanFloors;

        public static DesignationDef PlanFloorsColored;

        public static DesignationDef PlanObjects;

        public static DesignationDef PlanObjectsColored;

        public static DesignationDef PlanWalls;
        
        public static DesignationDef PlanWallsColored;

        static DesignationDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DesignationDefOf));
        }
    }
}
