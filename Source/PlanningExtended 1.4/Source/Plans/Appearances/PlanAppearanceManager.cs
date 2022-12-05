using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Settings;

namespace PlanningExtended.Plans.Appearances
{
    internal class PlanAppearanceManager
    {
        readonly static List<PlanAppearance> planAppearances;
        public static List<PlanAppearance> PlanAppearances => planAppearances;

        readonly static PlanAppearance planDoor = CreatePlanAppearance(PlanDesignationType.PlanDoors);

        readonly static PlanAppearance planFloor = CreatePlanAppearance(PlanDesignationType.PlanFloors);

        readonly static PlanAppearance planObject = CreatePlanAppearance(PlanDesignationType.PlanObjects);

        readonly static PlanAppearance planWall = CreatePlanAppearance(PlanDesignationType.PlanWall);

        static PlanningSettings Settings => PlanningMod.Settings;

        public static event Action<PlanDesignationType, float> OpacityChanged;

        public static event Action<PlanDesignationType, PlanTextureSet> TextureSetChanged;

        static PlanAppearanceManager()
        {
            planAppearances = new[] { planDoor, planFloor, planObject, planWall }.ToList();
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

            foreach (PlanAppearance planAppearance in planAppearances)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planAppearance.Type)
                    planAppearance.Opacity = opacity;

            OpacityChanged?.Invoke(planDesignationType, opacityPercentage);

            Settings.SetOpacity(planDesignationType, opacity);
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
            foreach (PlanAppearance planConfig in planAppearances)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planConfig.Type)
                    planConfig.TextureSet = planTextureSet;

            TextureSetChanged?.Invoke(planDesignationType, planTextureSet);

            Settings.SetTextureSet(planDesignationType, planTextureSet);
        }

        static PlanAppearance CreatePlanAppearance(PlanDesignationType planDesignationType)
        {
            return new PlanAppearance(planDesignationType, Settings.GetOpacity(planDesignationType), Settings.GetTextureSet(planDesignationType));
        }
    }
}
