using System.Collections.Generic;
using PlanningExtended.Plans;
using PlanningExtended.Plans.Gui;
using PlanningExtended.Plans.Persistence;
using Verse;

namespace PlanningExtended.Designators
{
    public class LoadPlanDesignator : BaseClickDesignator
    {
        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetLastLoadedPlansMenuOptions();

        public LoadPlanDesignator()
            : base("LoadPlan")
        {
        }

        public override void Click()
        {
            Find.WindowStack.Add(new LoadPlanDialog());
        }

        List<FloatMenuOption> GetLastLoadedPlansMenuOptions()
        {
            List<FloatMenuOption> list = [];

            foreach (string planName in PlanningMod.Settings.Plan.LastLoadedPlans)
            {
                list.Add(new FloatMenuOption(planName, () =>
                {
                    if (PlanPersistenceManager.Load(planName, out PlanInfo planInfo))
                    {
                        PlanManager.SetCachedPlanLayout(planInfo.PlanLayout);
                        PlanningMod.Settings.Plan.AddLastLoadedPlan(planName);
                    }
                    else
                    {
                        PlanningMod.Settings.Plan.RemoveLastLoadedPlan(planName);
                    }
                }));
            }

            return list;
        }
    }
}
