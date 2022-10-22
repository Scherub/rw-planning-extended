using RimWorld;
using Verse;

namespace PlanningExtended
{
    [DefOf]
    public static class PlanningKeyBindingDefOf
    {
        public static KeyBindingDef Planning_Modifier;
        
        public static KeyBindingDef Planning_FlipHorizontal;

        public static KeyBindingDef Planning_FlipVertical;

        public static KeyBindingDef Planning_Undo;
        
        public static KeyBindingDef Planning_Redo;
        
        static PlanningKeyBindingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PlanningKeyBindingDefOf));
        }
    }
}
