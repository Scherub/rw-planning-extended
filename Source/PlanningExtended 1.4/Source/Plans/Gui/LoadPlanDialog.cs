using PlanningExtended.Plans.Persistence;
using RimWorld;
using Verse;

namespace PlanningExtended.Plans.Gui
{
    public class LoadPlanDialog : PlanListDialog
    {
        protected override string InteractButtonLabel => "LoadGameButton".Translate();

        public LoadPlanDialog()
        {
        }

        protected override void DoFileInteraction(string fileName)
        {
            if (!PlanPersistenceManager.Load(fileName, out PlanInfo planInfo))
            {
                // TODO: show error message
                Close();
                return;
            }

            PlanManager.SetCachedPlanLayout(planInfo.PlanLayout);

            PlanningMod.Settings.AddLastLoadedPlan(fileName);
            
            Close();
        }
    }
}
