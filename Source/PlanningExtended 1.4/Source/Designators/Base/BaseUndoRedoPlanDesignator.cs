using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Plans;
using PlanningExtended.Plans.Appearances;
using PlanningExtended.UndoRedo;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseUndoRedoPlanDesignator : BasePlanDesignator
    {
        protected virtual bool UseUndoRedo => true;

        protected bool OverwriteDesignation => (!Settings.useSkipInsteadOfReplaceAsDefault && !IsSkipExistingDesignationsKeyPressed) || (Settings.useSkipInsteadOfReplaceAsDefault && IsSkipExistingDesignationsKeyPressed);

        protected BaseUndoRedoPlanDesignator(string name)
            : base(name)
        {
        }

        public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
        {
            if (TutorSystem.TutorialMode && !TutorSystem.AllowAction(new EventPack(this.TutorTagDesignate, cells)))
                return;

            PlanLayout undoPlanLayout = CreateUndoPlanLayout(cells);

            bool result = DesignateMultiCellInternal(cells);

            CreateRedoPlanLayout(undoPlanLayout);

            Finalize(result);
        }

        protected virtual bool DesignateMultiCellInternal(IEnumerable<IntVec3> cells)
        {
            bool somethingSucceeded = false;
            bool flag = false;

            foreach (IntVec3 cell in cells)
            {
                if (CanDesignateCell(cell).Accepted)
                {
                    DesignateSingleCell(cell);

                    somethingSucceeded = true;

                    if (!flag)
                        flag = ShowWarningForCell(cell);
                }
            }

            return somethingSucceeded;
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();

            PlanAppearanceManager.SetIsPlanVisible(PlanDesignationType.Unknown, true);
        }

        protected PlanLayout CreateUndoPlanLayout(IEnumerable<IntVec3> cells, IntVec3 origin = new IntVec3())
        {
            if (!UseUndoRedo)
                return null;

            //origin -= planLayout.FindMostBottomRightCell().ToIntVec3;

            AreaDimensions areaDimensions = CellUtilities.DetermineAreaDimensions(cells);

            return CreateUndoPlanLayout(areaDimensions);
        }

        protected PlanLayout CreateUndoPlanLayout(AreaDimensions areaDimensions)
        {
            if (!UseUndoRedo)
                return null;

            return PlanLayoutUtilities.CreateCopy(areaDimensions, Map);
        }

        protected void CreateRedoPlanLayout(PlanLayout undoPlanLayout)
        {
            if (!UseUndoRedo || undoPlanLayout == null)
                return;

            PlanLayout redoPlanLayout = PlanLayoutUtilities.CreateCopy(undoPlanLayout.Dimensions, Map);

            UndoRedoManager.Add(undoPlanLayout, redoPlanLayout);
        }

        protected string GetSkipReplaceModeString()
        {
            if (Settings.useSkipInsteadOfReplaceAsDefault)
                return IsSkipExistingDesignationsKeyPressed ? "PlanningExtended.Replace".Translate() : "PlanningExtended.Skip".Translate();
            else
                return IsSkipExistingDesignationsKeyPressed ? "PlanningExtended.Skip".Translate() : "PlanningExtended.Replace".Translate();
        }
    }
}
