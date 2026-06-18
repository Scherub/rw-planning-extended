using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Plans;
using PlanningExtended.UndoRedo;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators;

public abstract class BaseUndoRedoPlanDesignator : BasePlanDesignator
{
    protected virtual bool MakeAutoSnapshots => true;

    protected bool CanMakeAutoSnapshots => MakeAutoSnapshots;

    protected bool OverwriteDesignation => (!Settings.useSkipInsteadOfReplaceAsDefault && !IsSkipExistingDesignationsKeyPressed) || (Settings.useSkipInsteadOfReplaceAsDefault && IsSkipExistingDesignationsKeyPressed);

    protected BaseUndoRedoPlanDesignator(string name)
        : base(name)
    {
    }

    public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
    {
        if (TutorSystem.TutorialMode && !TutorSystem.AllowAction(new EventPack(TutorTagDesignate, cells)))
            return;

        PlanLayout undoPlanLayout = CanMakeAutoSnapshots ? CreateUndoPlanLayout(cells) : null;

        bool result = DesignateMultiCellInternal(cells);

        if (CanMakeAutoSnapshots && result)
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

    protected PlanLayout CreateUndoPlanLayout(IEnumerable<IntVec3> cells, IntVec3 origin = new IntVec3())
    {
        //origin -= planLayout.FindMostBottomRightCell().ToIntVec3;

        AreaDimensions areaDimensions = CellUtilities.DetermineAreaDimensions(cells);

        return CreateUndoPlanLayout(areaDimensions);
    }

    protected PlanLayout CreateUndoPlanLayout(AreaDimensions areaDimensions)
    {
        return PlanLayoutUtilities.CreateCopy(areaDimensions, Map);
    }

    protected void CreateRedoPlanLayout(PlanLayout undoPlanLayout)
    {
        if (undoPlanLayout == null)
            return;

        PlanLayout redoPlanLayout = PlanLayoutUtilities.CreateCopy(undoPlanLayout.Dimensions, Map);

        //Log.Message($"Created undo plan layout with {undoPlanLayout.Dimensions} dimensions and redo plan layout with {redoPlanLayout.Dimensions} dimensions.");

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
