using System.Collections.Generic;
using System.Linq;
using Verse;

namespace PlanningExtended
{
    public static class DesignationManagerExtensions
    {
        public static bool HasPlanDesignationAt(this DesignationManager designationManager, IntVec3 c)
        {
            return designationManager.GetPlanDesignationAt(c) != null;

            //List<Designation> designations = designationManager.GetPlanDesignationsAt(c);

            ////Log.Warning($"{designations.Count} of {DesignationDefOf.DesignationDefs.Count} possible designations found.");

            //return designations.Any(d => DesignationDefOf.DesignationDefs.Contains(d.def));
        }

        public static void RemovePlanDesignationsAt(this DesignationManager designationManager, IntVec3 c)
        {
            List<Designation> designations = designationManager.AllDesignationsAt(c);

            foreach (var designationDef in DesignationDefOf.DesignationDefs)
                designations.FirstOrDefault(d => d.def == designationDef)?.Delete();
        }

        //static List<Designation> GetPlanDesignationsAt(this DesignationManager designationManager, IntVec3 c)
        //{
        //    return designationManager.AllDesignationsAt(c).Where(d => DesignationDefOf.DesignationDefs.Contains(d.def)).ToList();
        //}

        public static Designation GetPlanDesignationAt(this DesignationManager designationManager, IntVec3 c)
        {
            return designationManager.AllDesignationsAt(c).Where(d => DesignationDefOf.DesignationDefs.Contains(d.def)).FirstOrDefault();
        }
    }
}
