namespace PlanningExtended.Plans.Appearances
{
    public class PlanAppearance
    {
        public PlanDesignationType Type { get; }

        public float Opacity { get; set; }

        public PlanTextureSet TextureSet { get; set; }

        public PlanAppearance(PlanDesignationType type, float opacity, PlanTextureSet textureSet)
        {
            Type = type;
            Opacity = opacity;
            TextureSet = textureSet;
        }
    }
}
