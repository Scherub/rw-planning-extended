using PlanningExtended.Plans;
using UnityEngine;

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
        }
    }
}
