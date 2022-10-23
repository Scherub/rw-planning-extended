using System.Linq;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Updates
{
    internal static class UpdatePlanDesignations
    {
        public static void Update()
        {
            Map map = Find.CurrentMap;

            foreach (IntVec3 cell in map.AllCells)
            {
                Designation designation = map.designationManager.AllDesignationsAt(cell).Where(d => d is Designation && PlanningDesignationDefOf.AllDesignationDefs.Contains(d.def)).FirstOrDefault();

                if (designation == null)
                    continue;

                map.designationManager.RemoveDesignation(designation);

                map.designationManager.AddDesignation(new PlanDesignation(designation.target, designation.def, designation.colorDef));
            }
        }
    }
}
