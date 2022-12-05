using System.Collections.Generic;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Settings
{
    public class PlanningSettings : ModSettings
    {
        Dictionary<PlanDesignationType, PlanDesignationSetting> planDesignationSettings = new();

        List<string> lastLoadedPlans = new();
        public List<string> LastLoadedPlans => lastLoadedPlans;

        public bool useUndoRedo = Default.UseUndoRedo;

        public int maxUndoOperations = Default.MaxUndoRedoSteps;

        public bool displayCutDesignator = Default.DisplayCutDesignator;

        public bool displayChangePlanAppearanceDesignator = Default.DisplayChangePlanAppearanceDesignator;

        public bool displayTogglePlanVisibilityDesignator = Default.DisplayTogglePlanVisibilityDesignator;

        public bool areDesignationsPersistent = Default.AreDesignationsPersistent;

        public bool useCtrlForColorDialog = Default.UseCtrlForColorDialog;

        public bool useSkipInsteadOfReplaceAsDefault = Default.UseSkipInsteadOfReplaceAsDefault;

        public bool alwaysGrabBottom = Default.AlwaysGrabBottom;

        public string paintPlanColor = Default.PaintPlanColor;

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
            Scribe_Values.Look(ref paintPlanColor, nameof(paintPlanColor), Default.PaintPlanColor);
            //Scribe_Values.Look(ref alwaysGrabBottom, nameof(alwaysGrabBottom), false);

            Scribe_Collections.Look(ref lastLoadedPlans, nameof(lastLoadedPlans));
            Scribe_Collections.Look(ref planDesignationSettings, nameof(planDesignationSettings), LookMode.Value, LookMode.Deep);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
                InitData();

            base.ExposeData();
        }

        public void Reset()
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

        public void SetOpacity(PlanDesignationType planDesignationType, float opacity, bool autoSave = true)
        {
            foreach (PlanDesignationSetting planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.opacity = opacity;

            if (autoSave)
                Write();
        }

        public float GetOpacity(PlanDesignationType planDesignationType)
        {
            return planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSetting planDesignationSetting) ? planDesignationSetting.opacity : 1f;
        }

        public void SetColor(PlanDesignationType planDesignationType, string color, bool autoSave = true)
        {
            foreach (PlanDesignationSetting planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.color = color;

            if (autoSave)
                Write();
        }

        public string GetColor(PlanDesignationType planDesignationType)
        {
            if (planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSetting planDesignationSetting))
                if (!string.IsNullOrEmpty(planDesignationSetting.color))
                    return planDesignationSetting.color;

            return ColorDefinitions.DefaultColorName;
        }

        public void SetTextureSet(PlanDesignationType planDesignationType, PlanTextureSet textureSet, bool autoSave = true)
        {
            foreach (PlanDesignationSetting planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.textureSet = textureSet;

            if (autoSave)
                Write();
        }

        public PlanTextureSet GetTextureSet(PlanDesignationType planDesignationType)
        {
            return planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSetting planDesignationSetting) ? planDesignationSetting.textureSet : PlanTextureSet.Dashed;
        }

        public void AddLastLoadedPlan(string planName, bool autoSave = true)
        {
            lastLoadedPlans.RemoveAll(p => p == planName);
            lastLoadedPlans.Add(planName);

            if (lastLoadedPlans.Count > 10)
                lastLoadedPlans.RemoveAt(0);

            if (autoSave)
                Write();
        }

        public void RemoveLastLoadedPlan(string planName, bool autoSave = true)
        {
            lastLoadedPlans.RemoveAll(p => p == planName);

            if (autoSave)
                Write();
        }

        void InitData()
        {
            lastLoadedPlans ??= new();
            planDesignationSettings ??= new();

            foreach (PlanDesignationType planDesignationType in PlanDesignationUtilities.GetPlanDesignationTypes())
                if (!planDesignationSettings.ContainsKey(planDesignationType))
                    planDesignationSettings[planDesignationType] = new PlanDesignationSetting(1f, "", PlanTextureSet.Dashed);
        }

        IEnumerable<PlanDesignationSetting> GetPlanDesignationSettings(PlanDesignationType planDesignationType)
        {
            if (planDesignationType == PlanDesignationType.Unknown)
            {
                foreach (PlanDesignationSetting planDesignationSetting in planDesignationSettings.Values)
                    yield return planDesignationSetting;
            }
            else
            {
                if (planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSetting planDesignationSetting))
                    yield return planDesignationSetting;
            }
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

            public const string PaintPlanColor = ColorDefinitions.DefaultColorName;
        }
    }
}
