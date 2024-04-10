using System;
using PlanningExtended.Plans.Persistence;
using RimWorld;
using Verse;

namespace PlanningExtended.Plans.Gui
{
    public class SavePlanDialog : PlanListDialog
    {
        readonly PlanLayout planLayout;

        protected override bool DisplayTypeInField => true;

        protected override string InteractButtonLabel => "OverwriteButton".Translate();

        public SavePlanDialog(PlanLayout planLayout)
        {
            this.planLayout = planLayout;
        }

        protected override void DoFileInteraction(string fileName)
        {
            PlanInfo planInfo = new(fileName, DateTime.Now, planLayout);

            fileName = GenFile.SanitizedFileName(fileName);

            LongEventHandler.QueueLongEvent(delegate ()
            {
                PlanPersistenceManager.Save(planInfo, fileName);
            }, "SavingLongEvent", false, null, true);

            Messages.Message("PlanningExtended.SavedPlanInfoAs".Translate(fileName), MessageTypeDefOf.SilentInput, false);

            Close();
        }
    }
}
