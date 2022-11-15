using System.Collections.Generic;
using PlanningExtended.Plans.Gui;
using UnityEngine;
using Verse;
using PlanningExtended.Plans.Persistence;
using PlanningExtended.Plans;

namespace PlanningExtended.Designators
{
    public class LoadPlanDesignator : BaseClickDesignator
    {
        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetLastLoadedPlansMenuOptions();

        public LoadPlanDesignator()
            : base("LoadPlan")
        {

        }

        public override void ProcessInput(Event ev)
        {
            Find.WindowStack.Add(new LoadPlanDialog());
        }

        List<FloatMenuOption> GetLastLoadedPlansMenuOptions()
        {
            List<FloatMenuOption> list = new();

            foreach (string planName in PlanningMod.Settings.LastLoadedPlans)
            {
                list.Add(new FloatMenuOption(planName, () =>
                {
                    if (PlanPersistenceManager.Load(planName, out PlanInfo planInfo))
                    {
                        PlanManager.SetCachedPlanLayout(planInfo.PlanLayout);
                        PlanningMod.Settings.AddLastLoadedPlan(planName);
                    }
                    else
                    {
                        PlanningMod.Settings.RemoveLastLoadedPlan(planName);
                    }
                }));
            }

            return list;
        }
    }
}
