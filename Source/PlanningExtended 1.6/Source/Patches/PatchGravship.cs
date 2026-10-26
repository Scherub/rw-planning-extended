using HarmonyLib;
using PlanningExtended.Gravships;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace PlanningExtended.Patches;

// The constructor captures the ship's cells. Vanilla removes all designations on them right afterwards.
[HarmonyPatch(typeof(Gravship), MethodType.Constructor, typeof(Building_GravEngine))]
class PatchGravshipConstructor
{
    static void Postfix(Gravship __instance)
    {
        if (ModsConfig.OdysseyActive)
            GravshipPlanTransfer.Capture(__instance);
    }
}

[HarmonyPatch(typeof(Gravship), nameof(Gravship.ExposeData))]
class PatchGravshipExposeData
{
    static void Postfix(Gravship __instance)
    {
        GravshipPlanTransfer.ExposeData(__instance);
    }
}

[HarmonyPatch(typeof(GravshipPlacementUtility), nameof(GravshipPlacementUtility.PlaceGravshipInMap))]
class PatchGravshipPlacementUtility
{
    static void Postfix(Gravship gravship, IntVec3 root, Map map)
    {
        if (ModsConfig.OdysseyActive)
            GravshipPlanTransfer.Restore(gravship, root, map);
    }
}
