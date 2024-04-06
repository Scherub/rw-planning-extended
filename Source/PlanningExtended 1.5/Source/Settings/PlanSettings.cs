using System.Collections.Generic;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Settings
{
    public class PlanSettings : BaseSettings
    {
        Dictionary<PlanDesignationType, PlanDesignationSettings> planDesignationSettings = [];

        List<string> lastLoadedPlans = [];
        public List<string> LastLoadedPlans => lastLoadedPlans;

        public PlanSettings(PlanningSettings planningSettings)
            : base(planningSettings)
        {
        }

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref lastLoadedPlans, nameof(lastLoadedPlans));
            Scribe_Collections.Look(ref planDesignationSettings, nameof(planDesignationSettings), LookMode.Value, LookMode.Deep);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
                InitData();
        }

        void InitData()
        {
            lastLoadedPlans ??= [];
            planDesignationSettings ??= [];

            foreach (PlanDesignationType planDesignationType in PlanDesignationUtilities.GetPlanDesignationTypes())
                if (!planDesignationSettings.ContainsKey(planDesignationType))
                    planDesignationSettings[planDesignationType] = new PlanDesignationSettings(1f, ColorDefinitions.DefaultColorName, PlanTextureSet.Round, true);
        }

        public void SetPlanColor(PlanDesignationType planDesignationType, string color, bool autoSave = true)
        {
            foreach (PlanDesignationSettings planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.color = color;

            if (autoSave)
                PlanningSettings.Write();
        }

        public string GetPlanColor(PlanDesignationType planDesignationType)
        {
            if (planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSettings planDesignationSetting))
                if (!string.IsNullOrEmpty(planDesignationSetting.color))
                    return planDesignationSetting.color;

            return ColorDefinitions.DefaultColorName;
        }

        public void SetPlanOpacity(PlanDesignationType planDesignationType, float opacity, bool autoSave = true)
        {
            foreach (PlanDesignationSettings planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.opacity = opacity;

            if (autoSave)
                PlanningSettings.Write();
        }

        public float GetPlanOpacity(PlanDesignationType planDesignationType)
        {
            return planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSettings planDesignationSetting) ? planDesignationSetting.opacity : 1f;
        }

        public void SetPlanTextureSet(PlanDesignationType planDesignationType, PlanTextureSet textureSet, bool autoSave = true)
        {
            foreach (PlanDesignationSettings planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.textureSet = textureSet;

            if (autoSave)
                PlanningSettings.Write();
        }

        public PlanTextureSet GetPlanTextureSet(PlanDesignationType planDesignationType)
        {
            return planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSettings planDesignationSetting) ? planDesignationSetting.textureSet : PlanTextureSet.Round;
        }

        public void SetPlanVisibility(PlanDesignationType planDesignationType, bool isVisible, bool autoSave = true)
        {
            foreach (PlanDesignationSettings planDesignationSetting in GetPlanDesignationSettings(planDesignationType))
                planDesignationSetting.isVisible = isVisible;

            if (autoSave)
                PlanningSettings.Write();
        }

        public bool GetPlanVisibility(PlanDesignationType planDesignationType)
        {
            return planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSettings planDesignationSetting) ? planDesignationSetting.isVisible : true;
        }

        public void AddLastLoadedPlan(string planName, bool autoSave = true)
        {
            lastLoadedPlans.RemoveAll(p => p == planName);
            lastLoadedPlans.Add(planName);

            if (lastLoadedPlans.Count > 10)
                lastLoadedPlans.RemoveAt(0);

            if (autoSave)
                PlanningSettings.Write();
        }

        public void RemoveLastLoadedPlan(string planName, bool autoSave = true)
        {
            lastLoadedPlans.RemoveAll(p => p == planName);

            if (autoSave)
                PlanningSettings.Write();
        }

        IEnumerable<PlanDesignationSettings> GetPlanDesignationSettings(PlanDesignationType planDesignationType)
        {
            if (planDesignationType == PlanDesignationType.Unknown)
            {
                foreach (PlanDesignationSettings planDesignationSetting in planDesignationSettings.Values)
                    yield return planDesignationSetting;
            }
            else
            {
                if (planDesignationSettings.TryGetValue(planDesignationType, out PlanDesignationSettings planDesignationSetting))
                    yield return planDesignationSetting;
            }
        }
    }
}
