using HarmonyLib;
using PlanningExtended.Plans;
using RimWorld;
using Verse;
using Verse.Sound;

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

            CheckKeyBindingToggle(PlanningKeyBindingDefOf.Planning_TogglePlanVisibility, ref arePlansVisible);

            PlanManager.SetArePlansVisible(arePlansVisible);
        }

        static void CheckKeyBindingToggle(KeyBindingDef keyBinding, ref bool value)
        {
            if (keyBinding.KeyDownEvent)
            {
                value = !value;

                if (value)
                    SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
                else
                    SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
            }
        }
    }
}
