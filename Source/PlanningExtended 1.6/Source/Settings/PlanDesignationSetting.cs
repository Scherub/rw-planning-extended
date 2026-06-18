using Verse;

namespace PlanningExtended.Settings;

public class PlanDesignationSetting : IExposable
{
    public float opacity;

    public string color;

    public PlanTextureSet textureSet;

    public bool isVisible;

    public PlanDesignationSetting()
    {

    }

    public PlanDesignationSetting(float opacity, string color, PlanTextureSet textureSet, bool isVisible)
    {
        this.opacity = opacity;
        this.color = color;
        this.textureSet = textureSet;
        this.isVisible = isVisible;
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref opacity, nameof(opacity), 1f);
        Scribe_Values.Look(ref color, nameof(color), "");
        Scribe_Values.Look(ref textureSet, nameof(textureSet), PlanTextureSet.Round);
        Scribe_Values.Look(ref isVisible, nameof(isVisible), true);
    }
}
