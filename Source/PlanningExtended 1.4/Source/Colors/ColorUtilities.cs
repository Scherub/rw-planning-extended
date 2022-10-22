using System.Linq;
using RimWorld;

namespace PlanningExtended.Colors
{
    public static class ColorUtilities
    {
        public static ColorDef GetColorDefByName(string defName)
        {
            return ColorDefinitions.ColorDefs.FirstOrDefault(c => c.defName == defName);
        }
    }
}
