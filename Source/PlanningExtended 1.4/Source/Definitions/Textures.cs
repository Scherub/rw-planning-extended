using UnityEngine;
using Verse;

namespace PlanningExtended
{
    [StaticConstructorOnStartup]
    public static class Textures
    {
        public static readonly Texture2D ShowPlanToggleIcon = ContentFinder<Texture2D>.Get("UI/ShowPlanToggle", true);
    }
}
