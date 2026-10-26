using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PlanningExtended.Patches;

/// <summary>
/// Hides the vanilla add / remove plan designators unless enabled in the settings.
/// Using exact type checks, because other vanilla plan designators (copy, expand, shrink) derive from these.
/// Using the GameRules.SetAllowDesignator method would make the change permanent in save files, so we patch the rules check instead.
/// </summary>
[HarmonyPatch(typeof(GameRules), nameof(GameRules.DesignatorAllowed))]
class PatchGameRulesDesignatorAllowed
{
    static void Postfix(Designator d, ref bool __result)
    {
        if (!__result || d == null || PlanningMod.Settings.displayVanillaPlanDesignators)
            return;

        Type type = d.GetType();

        if (type == typeof(Designator_Plan_Add) || type == typeof(Designator_Plan_Remove))
            __result = false;
    }
}
