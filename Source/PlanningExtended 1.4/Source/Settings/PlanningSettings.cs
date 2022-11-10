using Verse;

namespace PlanningExtended.Settings
{
    public class PlanningSettings : ModSettings
    {
        public const bool UseUndoRedo = true;

        public const int MaxUndoRedoSteps = 20;

        public const bool DisplayCutDesignator = true;

        public const bool DisplayTogglePlanVisibilityDesignator = false;

        public const bool AreDesignationsPersistent = true;

        public const bool AlwaysGrabBottom = false;

        public const bool UseCtrlForColorDialog = false;

        public bool useUndoRedo = UseUndoRedo;

        public int maxUndoOperations = MaxUndoRedoSteps;

        public bool displayCutDesignator = DisplayCutDesignator;

        public bool displayTogglePlanVisibilityDesignator = DisplayTogglePlanVisibilityDesignator;

        public bool areDesignationsPersistent = AreDesignationsPersistent;

        public bool alwaysGrabBottom = AlwaysGrabBottom;

        public bool useCtrlForColorDialog;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref useUndoRedo, nameof(useUndoRedo), UseUndoRedo);
            Scribe_Values.Look(ref maxUndoOperations, nameof(maxUndoOperations), MaxUndoRedoSteps);
            Scribe_Values.Look(ref displayCutDesignator, nameof(displayCutDesignator), DisplayCutDesignator);
            Scribe_Values.Look(ref displayTogglePlanVisibilityDesignator, nameof(displayTogglePlanVisibilityDesignator), DisplayTogglePlanVisibilityDesignator);
            Scribe_Values.Look(ref areDesignationsPersistent, nameof(areDesignationsPersistent), AreDesignationsPersistent);
            Scribe_Values.Look(ref useCtrlForColorDialog, nameof(useCtrlForColorDialog), UseCtrlForColorDialog);
            //Scribe_Values.Look(ref alwaysGrabBottom, nameof(alwaysGrabBottom), false);

            base.ExposeData();
        }

        public void Reset()
        {
            useUndoRedo = UseUndoRedo;
            maxUndoOperations = MaxUndoRedoSteps;
            displayCutDesignator = DisplayCutDesignator;
            displayTogglePlanVisibilityDesignator = DisplayTogglePlanVisibilityDesignator;
            areDesignationsPersistent = AreDesignationsPersistent;
            useCtrlForColorDialog = UseCtrlForColorDialog;
            alwaysGrabBottom = AlwaysGrabBottom;
        }
    }
}
