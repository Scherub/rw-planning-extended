using PlanningExtended.UndoRedo;
using RimWorld;
using UnityEngine;
using Verse.Sound;

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

            SoundDefOf.Tick_Tiny.PlayOneShotOnCamera(null);
        }

        void UndoRedoManager_OnChanged()
        {
            disabled = !UndoRedoManager.CanRedo;
        }
    }
}
