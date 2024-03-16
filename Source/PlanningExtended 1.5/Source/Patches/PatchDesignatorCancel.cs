using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using PlanningExtended.Designations;
using RimWorld;
using Verse;

namespace PlanningExtended.Patches
{
    [HarmonyPatch(typeof(Designator_Cancel), "CancelableDesignationsAt")]
    class PatchDesignatorCancel
    {
        public static IEnumerable<Designation> Postfix(IEnumerable<Designation> result)
        {
            return result.Where(d => d is not PlanDesignation);
        }
    }
}
