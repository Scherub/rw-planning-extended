using HarmonyLib;
#if RIMWORLD_1_5
using PlanningExtended.Gui.Toolbox;
#endif
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

            bool arePlansVisible = PlanAppearanceManager.AreAllPlansVisible;
#if RIMWORLD_1_5
            bool isToolboxVisible = ToolboxManager.IsToolboxVisible;
#endif
            row.ToggleableIcon(ref arePlansVisible, Textures.ShowPlanToggleIcon, "PlanningExtended.Settings.ArePlansVisible.Label".Translate(), SoundDefOf.Mouseover_ButtonToggle);
#if RIMWORLD_1_5
            row.ToggleableIcon(ref isToolboxVisible, Textures.ShowPlanToolboxIcon, "PlanningExtended.Settings.IsToolboxVisible.Label".Translate(), SoundDefOf.Mouseover_ButtonToggle);
#endif
            if (arePlansVisible != PlanAppearanceManager.AreAllPlansVisible)
                PlanAppearanceManager.SetPlanVisibility(PlanDesignationType.Unknown, arePlansVisible);

#if RIMWORLD_1_5
            if (isToolboxVisible != ToolboxManager.IsToolboxVisible)
                ToolboxManager.ShowToolboxWindow(isToolboxVisible);
#endif
            if (PlanningKeyBindingDefOf.Planning_TogglePlanVisibility.KeyDownEvent)
                PlanAppearanceManager.ToggleGlobalPlanVisibility();
#if RIMWORLD_1_5
            if (PlanningKeyBindingDefOf.Planning_TogglePlanningToolbox.KeyDownEvent)
                ToolboxManager.ShowToolboxWindow(!isToolboxVisible);
#endif
        }
    }
}
