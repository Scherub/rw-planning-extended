namespace PlanningExtended.Plans.Appearances
{
    public class PlanAppearance
    {
        public PlanDesignationType Type { get; }

        public string ColorDefName { get; set; }

        public float Opacity { get; set; }

        public PlanTextureSet TextureSet { get; set; }

        public bool IsVisible { get; set; }

        public PlanAppearance(PlanDesignationType type, string colorDefName, float opacity, PlanTextureSet textureSet, bool isVisible)
        {
            Type = type;
            ColorDefName = colorDefName;
            Opacity = opacity;
            TextureSet = textureSet;
            IsVisible = isVisible;
        }
    }
}
