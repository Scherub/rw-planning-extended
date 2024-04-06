using Verse;

namespace PlanningExtended.Settings
{
    public class PlanDesignationSettings : IExposable
    {
        public float opacity;

        public string color;

        public PlanTextureSet textureSet;

        public bool isVisible;

        public PlanDesignationSettings()
        {

        }

        public PlanDesignationSettings(float opacity, string color, PlanTextureSet textureSet, bool isVisible)
        {
            this.opacity = opacity;
            this.color = color;
            this.textureSet = textureSet;
            this.isVisible = isVisible;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref opacity, nameof(opacity), 1f);
            Scribe_Values.Look(ref color, nameof(color), ColorDefinitions.DefaultColorName);
            Scribe_Values.Look(ref textureSet, nameof(textureSet), PlanTextureSet.Round);
            Scribe_Values.Look(ref isVisible, nameof(isVisible), true);
        }
    }
}
