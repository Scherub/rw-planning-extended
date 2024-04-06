using UnityEngine;
using Verse;

namespace PlanningExtended
{
    [StaticConstructorOnStartup]
    public static class Textures
    {
        public static readonly Texture2D ShowPlanToggleIcon = ContentFinder<Texture2D>.Get("UI/ShowPlanToggle", true);

        public static readonly Texture2D ShowPlanToolboxIcon = ContentFinder<Texture2D>.Get("UI/ShowPlanToggle", true);

        public static readonly Texture2D RotateLeft = ContentFinder<Texture2D>.Get("UI/RotateLeft", true);
        
        public static readonly Texture2D RotateRight = ContentFinder<Texture2D>.Get("UI/RotateRight", true);
        
        public static readonly Texture2D FlipHorizontal = ContentFinder<Texture2D>.Get("UI/FlipHorizontal", true);
        
        public static readonly Texture2D FlipVertical = ContentFinder<Texture2D>.Get("UI/FlipVertical", true);
    }
}
