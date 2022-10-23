using PlanningExtended.Cells;
using PlanningExtended.Plans;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Designators
{
    public class CutPlanDesignator : BaseUndoRedoPlanDesignator
    {
        public override bool Visible => PlanningMod.Settings.displayCutDesignator;

        public CutPlanDesignator()
            : base("CutPlan")
        {
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c))
                return false;

            return Map.designationManager.HasPlanDesignationAt(c);
        }

        protected override bool DesignateMultiCellInternal(IEnumerable<IntVec3> cells)
        {
            CellArea cellArea = new(cells);

            if (cellArea.IsEmpty)
            {
                Messages.Message("PlanningExtended.NoPlanningDesignationsFound".Translate(), MessageTypeDefOf.RejectInput);
                return new AcceptanceReport("PlanningExtended.NoPlanningDesignationsFound".Translate());
            }

            PlanLayout planLayout = PlanLayoutUtilities.Create(cellArea, Map);

            CellUtilities.ClearCells(cellArea.Dimensions, Map);

            PlanManager.SetCachedPlanLayout(planLayout);

            Messages.Message("PlanningExtended.PlanningDesignationsCopied".Translate(), MessageTypeDefOf.NeutralEvent);

            return true;
        }

        protected override string GetMouseAttachmentText()
        {
            return $"{"PlanningExtended.Mode".Translate()}: {"PlanningExtended.Cut".Translate()}";
        }
    }
}
