using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Plans;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class CopyPlanDesignator : BasePlanDesignator
    {
        readonly CutPlanDesignator cutPlanDesignator;

        public CopyPlanDesignator()
            : base("CopyPlan")
        {
            cutPlanDesignator = new CutPlanDesignator();
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c))
                return false;

            return Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
        {
            if (IsModifierKeyPressed)
            {
                cutPlanDesignator.DesignateMultiCell(cells);
                return;
            }

            CellArea cellArea = new(cells);

            if (cellArea.IsEmpty)
            {
                Messages.Message("PlanningExtended.NoPlanningDesignationsFound".Translate(), MessageTypeDefOf.RejectInput);
                return;
            }

            PlanLayout planLayout = PlanLayoutUtilities.Create(cellArea, Map);

            PlanManager.SetCachedPlanLayout(planLayout);

            Messages.Message("PlanningExtended.PlanningDesignationsCopied".Translate(), MessageTypeDefOf.NeutralEvent);
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();

            PlanManager.SetIsPlanVisible(true);
        }

        public override void DrawMouseAttachments()
        {
            CheckPressedKeys();

            if (IsModifierKeyPressed)
            {
                cutPlanDesignator.DrawMouseAttachments();
                return;
            }

            base.DrawMouseAttachments();
        }

        protected override string GetMouseAttachmentText()
        {
            return $"{"PlanningExtended.Mode".Translate()}: {"PlanningExtended.Copy".Translate()}\n{PlanningKeyBindingDefOf.Planning_Modifier.MainKeyLabel}: {"PlanningExtended.SwitchToCut".Translate()}";
        }
    }
}
