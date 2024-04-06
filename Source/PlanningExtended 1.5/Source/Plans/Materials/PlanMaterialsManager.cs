using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Defs;
using PlanningExtended.Designations;
using PlanningExtended.Plans.Appearances;
using Verse;

namespace PlanningExtended.Plans.Materials
{
    [StaticConstructorOnStartup]
    public static class PlanMaterialsManager
    {
        static PlanMaterialsManager()
        {
            UpdateMaterials();

            PlanAppearanceManager.OpacityChanged += PlanAppearanceManager_OpacityChanged;
            PlanAppearanceManager.TextureSetChanged += PlanAppearanceManager_TextureSetChanged;
        }

        static void UpdateMaterials()
        {
            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                PlanAppearance planAppearance = PlanAppearanceManager.PlanAppearanceStorage.PlanAppearances.FirstOrDefault(pc => pc.Type == designationDefContainer.Type);

                if (planAppearance is not null)
                    foreach (var designationDef in designationDefContainer.DesignationDefs)
                        designationDef.iconMat = MaterialPool.MatFrom($"Designations/{planAppearance.TextureSet}/{designationDefContainer.TextureName}", ShaderDatabase.MetaOverlay);
            }
        }

        static void UpdatePlanDesignations(PlanDesignationType planDesignationType, PlanDesignationUpdateType planDesignationUpdateType)
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
                        designation.InvokeUpdate(planDesignationType, planDesignationUpdateType);
                }
            }
        }

        static void PlanAppearanceManager_OpacityChanged(PlanDesignationType planDesignationType, float opacity)
        {
            UpdatePlanDesignations(planDesignationType, PlanDesignationUpdateType.Opacity);
        }

        static void PlanAppearanceManager_TextureSetChanged(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            UpdateMaterials();

            UpdatePlanDesignations(planDesignationType, PlanDesignationUpdateType.Material);
        }
    }
}
