using HarmonyLib;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Patches
{
    [HarmonyPatch(typeof(Designation), "ExposeData")]
    class PatchDesignation
    {
        static void Postfix(Designation __instance)
        {
            if (__instance is PlanDesignation planDesignation)
            {
                RotationDirection rotation = planDesignation.Rotation;

                Scribe_Values.Look(ref rotation, nameof(rotation));

                planDesignation.Rotation = rotation;
            }
        }
    }
}
