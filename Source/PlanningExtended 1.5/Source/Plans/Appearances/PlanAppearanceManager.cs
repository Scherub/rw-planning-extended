using System;
using System.Linq;
using PlanningExtended.Colors;
using PlanningExtended.Settings;
using RimWorld;
using Verse.Sound;

namespace PlanningExtended.Plans.Appearances
{
    internal static class PlanAppearanceManager
    {
        static PlanningSettings Settings => PlanningMod.Settings;

        static public PlanAppearanceStorage PlanAppearanceStorage { get; } = new();

        public static PlanVisibility GlobalPlanVisibility { get; private set; }

        public static bool AreAllPlansVisible => GlobalPlanVisibility is PlanVisibility.Visible;

        public static event Action<ColorDef> PaintPlanColorChanged;

        public static event Action<PlanDesignationType, ColorDef> PlanColorChanged;
        
        public static event Action<PlanDesignationType, float> OpacityChanged;

        public static event Action<PlanDesignationType, PlanTextureSet> TextureSetChanged;

        public static event Action<PlanDesignationType, bool> VisibilityChanged;

        public static event Action<PlanVisibility> GlobalVisibilityChanged;

        static PlanAppearanceManager()
        {
            GlobalPlanVisibility = DetermineGlobalVisibility();
        }

        public static void SetPaintPlanColor(ColorDef colorDef)
        {
            Settings.PaintPlan.SetColor(colorDef.defName);

            PaintPlanColorChanged?.Invoke(colorDef);
        }

        public static ColorDef GetPaintPlanColor()
        {
            return ColorUtilities.GetColorDefByName(Settings.PaintPlan.GetColor());
        }

        public static void SetPlanColor(PlanDesignationType planDesignationType, ColorDef colorDef)
        {
            string colorDefName = colorDef.defName;

            PlanAppearanceStorage.ModifyPlanAppearance(planDesignationType, planAppearance => planAppearance.ColorDefName = colorDefName);

            Settings.Plan.SetPlanColor(planDesignationType, colorDefName);

            PlanColorChanged?.Invoke(planDesignationType, colorDef);
        }

        public static string GetPlanColorDefName(PlanDesignationType planDesignationType)
        {
            return PlanAppearanceStorage.GetPlanAppearance(planDesignationType)?.ColorDefName ?? ColorDefinitions.DefaultColorName;
        }

        public static ColorDef GetPlanColor(PlanDesignationType planDesignationType)
        {
            return ColorUtilities.GetColorDefByName(GetPlanColorDefName(planDesignationType));
        }

        public static void SetPlanOpacity(PlanDesignationType planDesignationType, int opacityPercentage)
        {
            float opacity = opacityPercentage / 100f;

            PlanAppearanceStorage.ModifyPlanAppearance(planDesignationType, planAppearance => planAppearance.Opacity = opacity);

            Settings.Plan.SetPlanOpacity(planDesignationType, opacity);

            OpacityChanged?.Invoke(planDesignationType, opacityPercentage);
        }

        public static float GetPlanOpacity(PlanDesignationType planDesignationType)
        {
            return PlanAppearanceStorage.GetPlanAppearance(planDesignationType)?.Opacity ?? 1f;
        }

        public static void SetPlanTextureSet(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            PlanAppearanceStorage.ModifyPlanAppearance(planDesignationType, planAppearance => planAppearance.TextureSet = planTextureSet);

            Settings.Plan.SetPlanTextureSet(planDesignationType, planTextureSet);

            TextureSetChanged?.Invoke(planDesignationType, planTextureSet);
        }

        public static PlanTextureSet GetPlanTextureSet(PlanDesignationType planDesignationType)
        {
            return PlanAppearanceStorage.GetPlanAppearance(planDesignationType)?.TextureSet ?? PlanTextureSet.Round;
        }

        public static void SetPlanVisibility(PlanDesignationType planDesignationType, bool isVisible)
        {
            if (planDesignationType != PlanDesignationType.Unknown && isVisible == GetPlanVisibility(planDesignationType))
                return;

            PlanAppearanceStorage.ModifyPlanAppearance(planDesignationType, planAppearance =>
            {
                planAppearance.IsVisible = isVisible;
                VisibilityChanged?.Invoke(planDesignationType, isVisible);
            });

            Settings.Plan.SetPlanVisibility(planDesignationType, isVisible);

            PlanVisibility globalPlanVisibility = DetermineGlobalVisibility();

            if (globalPlanVisibility != GlobalPlanVisibility)
            {
                GlobalPlanVisibility = globalPlanVisibility;
                GlobalVisibilityChanged?.Invoke(globalPlanVisibility);
            }
        }

        public static bool GetPlanVisibility(PlanDesignationType planDesignationType)
        {
            return PlanAppearanceStorage.GetPlanAppearance(planDesignationType)?.IsVisible ?? true;
        }

        public static void TogglePlanVisibility(PlanDesignationType planDesignationType)
        {
            bool isVisible = GetPlanVisibility(planDesignationType);

            PlayPlanVisibilityOnOffSound(!isVisible);

            SetPlanVisibility(planDesignationType, !isVisible);
        }

        public static void ToggleGlobalPlanVisibility()
        {
            PlayPlanVisibilityOnOffSound(!AreAllPlansVisible);

            SetPlanVisibility(PlanDesignationType.Unknown, !AreAllPlansVisible);
        }

        static PlanVisibility DetermineGlobalVisibility()
        {
            if (PlanAppearanceStorage.PlanAppearances.All(p => p.IsVisible))
                return PlanVisibility.Visible;
            else if (PlanAppearanceStorage.PlanAppearances.Any(p => p.IsVisible))
                return PlanVisibility.PartiallyVisible;
            else
                return PlanVisibility.Invisible;
        }

        static void PlayPlanVisibilityOnOffSound(bool isVisible)
        {
            if (isVisible)
                SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
            else
                SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
        }
    }
}
