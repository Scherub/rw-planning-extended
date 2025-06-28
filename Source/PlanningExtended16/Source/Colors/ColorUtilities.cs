using RimWorld;
using Verse;

namespace PlanningExtended.Colors
{
    public static class ColorUtilities
    {
        public static ColorDef GetColorDefByName(string defName)
        {
            return ColorDefinitions.ColorDefs.FirstOrFallback(c => c.defName == defName, ColorDefinitions.NonColoredDef);
        }
    }
}
