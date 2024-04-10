using Verse;

namespace PlanningExtended.Settings
{
    public class PlanDesignationSetting : IExposable
    {
        public float opacity;

        public string color;

        public PlanTextureSet textureSet;

        public PlanDesignationSetting()
        {

        }

        public PlanDesignationSetting(float opacity, string color, PlanTextureSet textureSet)
        {
            this.opacity = opacity;
            this.color = color;
            this.textureSet = textureSet;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref opacity, nameof(opacity), 1f);
            Scribe_Values.Look(ref color, nameof(color), "");
            Scribe_Values.Look(ref textureSet, nameof(textureSet), PlanTextureSet.Round);
        }
    }
}
