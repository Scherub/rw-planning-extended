using PlanningExtended.UndoRedo;
using RimWorld;
using Verse.Sound;

namespace PlanningExtended.Designators
{
    public class RedoPlanDesignator : BaseClickDesignator
    {
        public override bool Visible => PlanningMod.Settings.General.useUndoRedo;

        public RedoPlanDesignator()
            : base("RedoPlan")
        {
            disabled = true;
            hotKey = PlanningKeyBindingDefOf.Planning_Redo;
            UndoRedoManager.OnChanged -= UndoRedoManager_OnChanged;
            UndoRedoManager.OnChanged += UndoRedoManager_OnChanged;
        }

        public override void Click()
        {
            UndoRedoManager.Redo(Map);

            SoundDefOf.Tick_Tiny.PlayOneShotOnCamera(null);
        }

        void UndoRedoManager_OnChanged()
        {
            disabled = !UndoRedoManager.CanRedo;
        }
    }
}
