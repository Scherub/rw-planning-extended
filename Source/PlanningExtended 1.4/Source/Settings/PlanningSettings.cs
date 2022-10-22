using Verse;

namespace PlanningExtended.Settings
{
    public class PlanningSettings : ModSettings
    {
        public const bool UseUndoRedo = true;
        
        public const int MaxUndoRedoSteps = 20;

        public const bool DisplayCutDesignator = true;

        public const bool AlwaysGrabBottom = false;

        public bool useUndoRedo = UseUndoRedo;

        public int maxUndoOperations = MaxUndoRedoSteps;

        public bool displayCutDesignator = DisplayCutDesignator;

        public bool alwaysGrabBottom = AlwaysGrabBottom;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref useUndoRedo, nameof(useUndoRedo), UseUndoRedo);
            Scribe_Values.Look(ref maxUndoOperations, nameof(maxUndoOperations), MaxUndoRedoSteps);
            Scribe_Values.Look(ref displayCutDesignator, nameof(displayCutDesignator), DisplayCutDesignator);
            //Scribe_Values.Look(ref alwaysGrabBottom, nameof(alwaysGrabBottom), false);

            base.ExposeData();
        }

        public void Reset()
        {
            useUndoRedo = UseUndoRedo;
            maxUndoOperations = MaxUndoRedoSteps;
            displayCutDesignator = DisplayCutDesignator;
            alwaysGrabBottom = AlwaysGrabBottom;
        }
    }
}
