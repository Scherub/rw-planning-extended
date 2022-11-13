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

        public static PlanConfig PlanDoor => new(PlanDesignitionType.PlanDoors, 1f, PlanTextureSet.Dashed);

        public static PlanConfig PlanFloor => new(PlanDesignitionType.PlanFloors, 1f, PlanTextureSet.Dashed);

        public static PlanConfig PlanObject => new(PlanDesignitionType.PlanObjects, 1f, PlanTextureSet.Dashed);

        public static PlanConfig PlanWall => new(PlanDesignitionType.PlanWall, 1f, PlanTextureSet.Dashed);

        public static void SetPlanOpacity(int planOpacityPercentage)
        {
            PlanOpacityPercentage = planOpacityPercentage;
            PlanOpacityAlpha = planOpacityPercentage / 100f;

            UpdatePlanDesignations(PlanDesignitionType.Unknown);
        }

        public static void SetPlanOpacity(int planOpacityPercentage, PlanDesignitionType planDesignitionType)
        {
            UpdatePlanDesignations(planDesignitionType);
        }

        public static void SetPlanTextureSet(PlanTextureSet planTextureSet)
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                foreach (var designationDef in designationDefContainer.DesignationDefs)
                {
                    designationDef.iconMat = MaterialPool.MatFrom($"Designations/{planTextureSet}/{designationDefContainer.TextureName}", ShaderDatabase.MetaOverlay);

                    List<PlanDesignation> designations = map.designationManager.designationsByDef[designationDef].Where(d => d is PlanDesignation).Select(d => d as PlanDesignation).ToList();

                    foreach (var designation in designations)
                        designation.InvokeMaterialUpdate(PlanDesignitionType.Unknown);
                }
            }
        }

        static void UpdatePlanDesignations(PlanDesignitionType planDesignitionType)
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
                        designation.InvokeColorUpdate(planDesignitionType);
                }
            }
        }

        public class PlanConfig
        {
            public PlanDesignitionType Type { get; }

            public float PlanOpacity { get; set; }

            public PlanTextureSet TextureSet { get; set; }

            public PlanConfig(PlanDesignitionType type, float planOpacity, PlanTextureSet textureSet)
            {
                Type = type;
                PlanOpacity = planOpacity;
                TextureSet = textureSet;
            }
        }
    }
}
