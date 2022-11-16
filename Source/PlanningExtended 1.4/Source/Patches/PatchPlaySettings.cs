using HarmonyLib;
using PlanningExtended.Plans;
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

            bool arePlansVisible = PlanManager.ArePlansVisible;

            row.ToggleableIcon(ref arePlansVisible, Textures.ShowPlanToggleIcon, "PlanningExtended.Settings.ArePlansVisible.Label".Translate(), SoundDefOf.Mouseover_ButtonToggle);

            if (arePlansVisible != PlanManager.ArePlansVisible)
                PlanManager.SetIsPlanVisible(arePlansVisible);

            if (PlanningKeyBindingDefOf.Planning_TogglePlanVisibility.KeyDownEvent)
                PlanManager.ToggleIsPlanVisible(PlanDesignationType.Unknown);
        }
    }
}
