using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse.Sound;

namespace PlanningExtended.Designators
{
    public class TogglePlanVisibilityDesignator : BaseClickDesignator
    {
        public override bool Visible => PlanningMod.Settings.displayTogglePlanVisibilityDesignator;

        public TogglePlanVisibilityDesignator()
            : base("TogglePlanVisibility")
        {
        }

        public override void ProcessInput(Event ev)
        {
            PlanManager.SetArePlansVisible(!PlanManager.ArePlansVisible);

            if (PlanManager.ArePlansVisible)
                SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
            else
                SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);

        }
    }
}
