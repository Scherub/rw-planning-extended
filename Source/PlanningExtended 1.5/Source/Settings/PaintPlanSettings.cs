using Verse;

namespace PlanningExtended.Settings
{
    public class PaintPlanSettings : BaseSettings
    {
        string color;

        public PaintPlanSettings(PlanningSettings planningSettings)
            : base(planningSettings)
        {
            color = ColorDefinitions.DefaultColorName;
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref color, nameof(color), ColorDefinitions.DefaultColorName);
        }

        public void SetColor(string color, bool autoSave = true)
        {
            this.color = color;

            if (autoSave)
                PlanningSettings.Write();
        }

        public string GetColor()
        {
            return color;
        }
    }
}
