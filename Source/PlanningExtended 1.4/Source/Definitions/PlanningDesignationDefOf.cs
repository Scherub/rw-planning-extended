using System.Collections.Generic;
using PlanningExtended.Defs;
using RimWorld;
using Verse;

namespace PlanningExtended
{
    [DefOf]
    public static class PlanningDesignationDefOf
    {
        static List<DesignationDefContainer> _designationDefs;
        public static List<DesignationDefContainer> DesignationDefs
        {
            get
            {
                _designationDefs ??= new(new[]
                {
                    new DesignationDefContainer("Wall", PlanWalls, PlanWallsColored),
                    new DesignationDefContainer("Floor", PlanFloors, PlanFloorsColored),
                    new DesignationDefContainer("Door", PlanDoors, PlanDoorsColored),
                    new DesignationDefContainer("Object", PlanObjects, PlanObjectsColored),
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
                    _allDesignationDefs = new(DesignationDefs.Count * 2 + 1)
                    {
                        DesignationDefOf.Plan,
                    };

                    foreach (DesignationDefContainer designationDefContainer in DesignationDefs)
                        _allDesignationDefs.AddRange(designationDefContainer.DesignationDefs);
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
