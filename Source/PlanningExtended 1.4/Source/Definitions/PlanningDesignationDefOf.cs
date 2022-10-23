using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PlanningExtended
{
    [DefOf]
    public static class PlanningDesignationDefOf
    {
        static List<DesignationDef> _designationDefs;
        public static List<DesignationDef> DesignationDefs
        {
            get
            {
                _designationDefs ??= new(new[]
                {
                    //RimWorldDesignationDefOf.Plan,
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

        static List<DesignationDef> _allDesignationDefs;
        public static List<DesignationDef> AllDesignationDefs
        {
            get
            {
                if (_allDesignationDefs == null)
                {
                    _allDesignationDefs = new(DesignationDefs.Count + 1)
                    {
                        DesignationDefOf.Plan,
                    };

                    _allDesignationDefs.AddRange(DesignationDefs);
                }

                return _allDesignationDefs;
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

        static PlanningDesignationDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PlanningDesignationDefOf));
        }
    }
}
