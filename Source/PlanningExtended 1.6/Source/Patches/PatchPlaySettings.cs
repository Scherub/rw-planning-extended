using HarmonyLib;
using PlanningExtended.Plans.Appearances;
using RimWorld;
using Verse;

namespace PlanningExtended.Patches
{
    [HarmonyPatch(typeof(PlaySettings), "DoPlaySettingsGlobalControls")]
    class PatchPlaySettings
    {
        static void Postfix(WidgetRow row, bool worldView)
        {
            if (worldView || row == null)
                return;

            bool arePlansVisible = PlanAppearanceManager.ArePlansVisible;

            row.ToggleableIcon(ref arePlansVisible, Textures.ShowPlanToggleIcon, "PlanningExtended.Settings.ArePlansVisible.Label".Translate(), SoundDefOf.Mouseover_ButtonToggle);

            if (arePlansVisible != PlanAppearanceManager.ArePlansVisible)
                PlanAppearanceManager.SetIsPlanVisible(PlanDesignationType.Unknown, arePlansVisible);

            if (PlanningKeyBindingDefOf.Planning_TogglePlanVisibility.KeyDownEvent)
                PlanAppearanceManager.ToggleIsPlanVisible(PlanDesignationType.Unknown);
        }
    }
}
