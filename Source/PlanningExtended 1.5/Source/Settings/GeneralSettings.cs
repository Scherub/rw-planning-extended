using Verse;

namespace PlanningExtended.Settings
{
    public class GeneralSettings : BaseSettings
    {
        public bool useUndoRedo = Default.UseUndoRedo;

        public int maxUndoOperations = Default.MaxUndoRedoSteps;

        public bool displayCutDesignator = Default.DisplayCutDesignator;

        public bool displayChangePlanAppearanceDesignator = Default.DisplayChangePlanAppearanceDesignator;

        public bool displayTogglePlanVisibilityDesignator = Default.DisplayTogglePlanVisibilityDesignator;

        public bool areDesignationsPersistent = Default.AreDesignationsPersistent;

        public bool useCtrlForColorDialog = Default.UseCtrlForColorDialog;

        public bool useSkipInsteadOfReplaceAsDefault = Default.UseSkipInsteadOfReplaceAsDefault;

        public bool alwaysGrabBottom = Default.AlwaysGrabBottom;

        public GeneralSettings(PlanningSettings planningSettings)
            : base(planningSettings)
        {
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref useUndoRedo, nameof(useUndoRedo), Default.UseUndoRedo);
            Scribe_Values.Look(ref maxUndoOperations, nameof(maxUndoOperations), Default.MaxUndoRedoSteps);
            Scribe_Values.Look(ref displayCutDesignator, nameof(displayCutDesignator), Default.DisplayCutDesignator);
            Scribe_Values.Look(ref displayChangePlanAppearanceDesignator, nameof(displayChangePlanAppearanceDesignator), Default.DisplayChangePlanAppearanceDesignator);
            Scribe_Values.Look(ref displayTogglePlanVisibilityDesignator, nameof(displayTogglePlanVisibilityDesignator), Default.DisplayTogglePlanVisibilityDesignator);
            Scribe_Values.Look(ref areDesignationsPersistent, nameof(areDesignationsPersistent), Default.AreDesignationsPersistent);
            Scribe_Values.Look(ref useCtrlForColorDialog, nameof(useCtrlForColorDialog), Default.UseCtrlForColorDialog);
            Scribe_Values.Look(ref useSkipInsteadOfReplaceAsDefault, nameof(useSkipInsteadOfReplaceAsDefault), Default.UseSkipInsteadOfReplaceAsDefault);
            //Scribe_Values.Look(ref alwaysGrabBottom, nameof(alwaysGrabBottom), false);
        }

        public override void Reset()
        {
            useUndoRedo = Default.UseUndoRedo;
            maxUndoOperations = Default.MaxUndoRedoSteps;
            displayCutDesignator = Default.DisplayCutDesignator;
            displayChangePlanAppearanceDesignator = Default.DisplayChangePlanAppearanceDesignator;
            displayTogglePlanVisibilityDesignator = Default.DisplayTogglePlanVisibilityDesignator;
            areDesignationsPersistent = Default.AreDesignationsPersistent;
            useCtrlForColorDialog = Default.UseCtrlForColorDialog;
            useSkipInsteadOfReplaceAsDefault = Default.UseSkipInsteadOfReplaceAsDefault;
            alwaysGrabBottom = Default.AlwaysGrabBottom;
        }

        class Default
        {
            public const bool UseUndoRedo = true;

            public const int MaxUndoRedoSteps = 20;

            public const bool DisplayCutDesignator = true;

            public const bool DisplayChangePlanAppearanceDesignator = true;

            public const bool DisplayTogglePlanVisibilityDesignator = true;

            public const bool AreDesignationsPersistent = true;

            public const bool AlwaysGrabBottom = false;

            public const bool UseCtrlForColorDialog = false;

            public const bool UseSkipInsteadOfReplaceAsDefault = false;
        }
    }
}
