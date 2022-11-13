using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Defs;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Materials
{
    public static class MaterialsManager
    {
        public static int PlanOpacityPercentage { get; private set; }
        
        public static float PlanOpacityAlpha { get; private set; }

        public static void SetPlanOpacity(int planOpacityPercentage)
        {
            PlanOpacityPercentage = planOpacityPercentage;
            PlanOpacityAlpha = planOpacityPercentage / 100f;

            UpdatePlanDesignations();
        }

        static void UpdatePlanDesignations()
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                foreach (DesignationDef designationDef in designationDefContainer.DesignationDefs)
                {
                    List<PlanDesignation> designations = map.designationManager.designationsByDef[designationDef].Where(d => d is PlanDesignation).Select(d => d as PlanDesignation).ToList();

                    foreach (var designation in designations)
                        designation.InvokeColorUpdate();
                }
            }
        }
    }
}
