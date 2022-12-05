namespace PlanningExtended.Plans.Appearances
{
    public class PlanAppearance
    {
        public PlanDesignationType Type { get; }

        public float Opacity { get; set; }

        public PlanTextureSet TextureSet { get; set; }

        public bool IsVisible { get; set; }

        public PlanAppearance(PlanDesignationType type, float opacity, PlanTextureSet textureSet, bool isVisible)
        {
            Type = type;
            Opacity = opacity;
            TextureSet = textureSet;
            IsVisible = isVisible;
        }
    }
}
