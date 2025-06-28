using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Designations;
using RimWorld;
using Verse;

namespace PlanningExtended.Plans.Converters
{
    internal static class VanillaPlanConverter
    {
        public static void Convert()
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            DesignationDef wallPlanDesignation = PlanningDesignationDefOf.PlanWalls;

            foreach (IntVec3 position in map.AllCells)
            {
                List<Designation> designations = map.designationManager.AllDesignationsAt(position);

                Designation designation = designations.FirstOrDefault(d => d.def == DesignationDefOf.Plan);

                if (designation == null)
                    continue;

                designation.Delete();

                PlanDesignationPlacerUtilities.Designate(map, position, wallPlanDesignation, ColorDefinitions.NonColoredDef);
            }
        }
    }
}
