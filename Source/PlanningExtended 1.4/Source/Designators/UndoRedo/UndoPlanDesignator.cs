using PlanningExtended.UndoRedo;
using UnityEngine;

namespace PlanningExtended.Designators
{
    public class UndoPlanDesignator : BaseClickDesignator
    {
        public override bool Visible => PlanningMod.Settings.useUndoRedo;

        public UndoPlanDesignator()
            : base("UndoPlan")
        {
            disabled = true;
            hotKey = PlanningKeyBindingDefOf.Planning_Undo;
            UndoRedoManager.OnChanged += UndoRedoManager_OnChanged;
        }

        public override void ProcessInput(Event ev)
        {
            UndoRedoManager.Undo(Map);
        }

        void UndoRedoManager_OnChanged()
        {
            disabled = !UndoRedoManager.CanUndo;
        }
    }
}
