using PlanningExtended.Plans;
using PlanningExtended.Plans.Gui;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class SavePlanDesignator : BaseClickDesignator
    {
        public SavePlanDesignator()
            : base("SavePlan")
        {
            disabled = true;

            PlanManager.OnCachedPlanLayoutChanged += PlanManager_OnCachedPlanLayoutChanged;
        }

        public override void ProcessInput(Event ev)
        {
            Find.WindowStack.Add(new SavePlanDialog(PlanManager.CachedPlanLayout));
        }

        void PlanManager_OnCachedPlanLayoutChanged(PlanLayout planLayout)
        {
            disabled = false;
        }
    }
}
