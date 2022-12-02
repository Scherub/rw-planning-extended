using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Defs;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Materials
{
    public static class MaterialsManager
    {
        readonly static List<PlanConfig> planConfigs;

        readonly static PlanConfig planDoor = CreatePlanConfig(PlanDesignationType.PlanDoors);

        readonly static PlanConfig planFloor = CreatePlanConfig(PlanDesignationType.PlanFloors);

        readonly static PlanConfig planObject = CreatePlanConfig(PlanDesignationType.PlanObjects);

        readonly static PlanConfig planWall = CreatePlanConfig(PlanDesignationType.PlanWall);

        public static Action<PlanDesignationType, PlanTextureSet> TextureSetChanged;

        static MaterialsManager()
        {
            planConfigs = new[] { planDoor, planFloor, planObject, planWall }.ToList();

            UpdateMaterials();
        }

        public static float GetPlanOpacity(PlanDesignationType planDesignationType)
        {
            return planDesignationType switch
            {
                PlanDesignationType.PlanDoors => planDoor.Opacity,
                PlanDesignationType.PlanFloors => planFloor.Opacity,
                PlanDesignationType.PlanObjects => planObject.Opacity,
                PlanDesignationType.PlanWall => planWall.Opacity,
                _ => 1f,
            };
        }

        public static void SetPlanOpacity(PlanDesignationType planDesignationType, int opacityPercentage)
        {
            float opacity = opacityPercentage / 100f;

            foreach (PlanConfig planConfig in planConfigs)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planConfig.Type)
                    planConfig.Opacity = opacity;

            UpdatePlanDesignations(planDesignationType, PlanDesignationUpdateType.Opacity);

            PlanningMod.Settings.SetOpacity(planDesignationType, opacity);
            PlanningMod.Settings.Write();
        }

        public static PlanTextureSet GetPlanTextureSet(PlanDesignationType planDesignationType)
        {
            return planDesignationType switch
            {
                PlanDesignationType.PlanDoors => planDoor.TextureSet,
                PlanDesignationType.PlanFloors => planFloor.TextureSet,
                PlanDesignationType.PlanObjects => planObject.TextureSet,
                PlanDesignationType.PlanWall => planWall.TextureSet,
                _ => PlanTextureSet.Dashed
            };
        }

        public static void SetPlanTextureSet(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            foreach (PlanConfig planConfig in planConfigs)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planConfig.Type)
                    planConfig.TextureSet = planTextureSet;

            UpdateMaterials();

            UpdatePlanDesignations(planDesignationType, PlanDesignationUpdateType.Material);

            TextureSetChanged?.Invoke(planDesignationType, planTextureSet);

            PlanningMod.Settings.SetTextureSet(planDesignationType, planTextureSet);
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

        static void UpdateMaterials()
        {
            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                PlanConfig planConfig = planConfigs.FirstOrDefault(pc => pc.Type == designationDefContainer.Type);

                if (planConfig is not null)
                    foreach (var designationDef in designationDefContainer.DesignationDefs)
                        designationDef.iconMat = MaterialPool.MatFrom($"Designations/{planConfig.TextureSet}/{designationDefContainer.TextureName}", ShaderDatabase.MetaOverlay);
            }
        }

        static PlanConfig CreatePlanConfig(PlanDesignationType planDesignationType)
        {
            return new PlanConfig(planDesignationType, PlanningMod.Settings.GetOpacity(planDesignationType), PlanningMod.Settings.GetTextureSet(planDesignationType));
        }

        public class PlanConfig
        {
            public PlanDesignationType Type { get; }

            public float Opacity { get; set; }

            public PlanTextureSet TextureSet { get; set; }

            public PlanConfig(PlanDesignationType type, float opacity, PlanTextureSet textureSet)
            {
                Type = type;
                Opacity = opacity;
                TextureSet = textureSet;
            }
        }
    }
}
