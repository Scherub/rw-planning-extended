using PlanningExtended.Plans.Gui;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class LoadPlanDesignator : BaseClickDesignator
    {
        public LoadPlanDesignator()
            : base("LoadPlan")
        {

        }

        public override void ProcessInput(Event ev)
        {
            Find.WindowStack.Add(new LoadPlanDialog());
        }
    }
}
