using Verse;

namespace PlanningExtended.Settings
{
    public class PlanningSettings : ModSettings
    {
        GeneralSettings generalSettings;
        public GeneralSettings General => generalSettings;

        PaintPlanSettings paintPlanSettings;
        public PaintPlanSettings PaintPlan => paintPlanSettings;

        PlanSettings planSettings;
        public PlanSettings Plan => planSettings;

        ToolboxSettings toolboxSettings;
        public ToolboxSettings Toolbox => toolboxSettings;

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref generalSettings, nameof(generalSettings), this);
            Scribe_Deep.Look(ref planSettings, nameof(planSettings), this);
            Scribe_Deep.Look(ref paintPlanSettings, nameof(paintPlanSettings), this);
            Scribe_Deep.Look(ref toolboxSettings, nameof(toolboxSettings), this);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
                InitData();

            base.ExposeData();
        }

        void InitData()
        {
            generalSettings ??= new GeneralSettings(this);
            paintPlanSettings ??= new PaintPlanSettings(this);
            planSettings ??= new PlanSettings(this);
            toolboxSettings ??= new ToolboxSettings(this);
        }

        public void Reset()
        {
            generalSettings.Reset();
            paintPlanSettings.Reset();
            planSettings.Reset();
            toolboxSettings.Reset();
        }
    }
}
