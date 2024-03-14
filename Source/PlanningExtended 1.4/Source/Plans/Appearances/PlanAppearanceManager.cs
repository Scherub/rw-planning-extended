using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Settings;
using RimWorld;
using Verse.Sound;

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

        public static PlanVisibility PlanVisibility { get; private set; }

        public static bool ArePlansVisible => PlanVisibility is PlanVisibility.Visible;

        public static event Action<PlanDesignationType, float> OpacityChanged;

        public static event Action<PlanDesignationType, PlanTextureSet> TextureSetChanged;

        public static event Action<PlanVisibility> VisibilityChanged;

        static PlanAppearanceManager()
        {
            planAppearances = [planDoor, planFloor, planObject, planWall];

            DetermineVisibility();
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
                _ => PlanTextureSet.Round
            };
        }

        public static void SetPlanTextureSet(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            foreach (PlanAppearance planAppearance in planAppearances)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planAppearance.Type)
                    planAppearance.TextureSet = planTextureSet;

            TextureSetChanged?.Invoke(planDesignationType, planTextureSet);

            Settings.SetTextureSet(planDesignationType, planTextureSet);
        }

        public static bool IsPlanVisible(PlanDesignationType planDesignationType)
        {
            return planDesignationType switch
            {
                PlanDesignationType.PlanDoors => planDoor.IsVisible,
                PlanDesignationType.PlanFloors => planFloor.IsVisible,
                PlanDesignationType.PlanObjects => planObject.IsVisible,
                PlanDesignationType.PlanWall => planWall.IsVisible,
                _ => ArePlansVisible,
            };
        }

        public static void SetIsPlanVisible(PlanDesignationType planDesignationType, bool isVisible)
        {
            if (isVisible == IsPlanVisible(planDesignationType))
                return;

            foreach (PlanAppearance planAppearance in planAppearances)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planAppearance.Type)
                    planAppearance.IsVisible = isVisible;

            DetermineVisibility();

            VisibilityChanged?.Invoke(PlanVisibility);
        }

        public static void ToggleIsPlanVisible(PlanDesignationType planDesignationType)
        {
            bool isVisible = !IsPlanVisible(planDesignationType);

            PlayPlanVisibilityOnOffSound(isVisible);

            SetIsPlanVisible(planDesignationType, isVisible);
        }

        static void DetermineVisibility()
        {
            if (planAppearances.All(p => p.IsVisible))
                PlanVisibility = PlanVisibility.Visible;
            else if (planAppearances.Any(p => p.IsVisible))
                PlanVisibility = PlanVisibility.PartiallyVisible;
            else
                PlanVisibility = PlanVisibility.Invisible;
        }

        static void PlayPlanVisibilityOnOffSound(bool isVisible)
        {
            if (isVisible)
                SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
            else
                SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
        }

        static PlanAppearance CreatePlanAppearance(PlanDesignationType planDesignationType)
        {
            return new PlanAppearance(planDesignationType, Settings.GetOpacity(planDesignationType), Settings.GetTextureSet(planDesignationType), true);
        }
    }
}
