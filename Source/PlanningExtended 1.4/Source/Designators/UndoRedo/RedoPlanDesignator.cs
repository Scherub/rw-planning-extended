using PlanningExtended.UndoRedo;
using UnityEngine;

namespace PlanningExtended.Designators
{
    public class RedoPlanDesignator : BaseClickDesignator
    {
        public override bool Visible => PlanningMod.Settings.useUndoRedo;

        public RedoPlanDesignator()
            : base("RedoPlan")
        {
            disabled = true;
            hotKey = PlanningKeyBindingDefOf.Planning_Redo;
            UndoRedoManager.OnChanged += UndoRedoManager_OnChanged;
        }

        public override void ProcessInput(Event ev)
        {
            UndoRedoManager.Redo(Map);
        }

        void UndoRedoManager_OnChanged()
        {
            disabled = !UndoRedoManager.CanRedo;
        }
    }
}
