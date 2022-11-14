using RimWorld;
using Verse;

namespace PlanningExtended
{
    [DefOf]
    public static class PlanningKeyBindingDefOf
    {
        public static KeyBindingDef Planning_Modifier;

        public static KeyBindingDef Planning_NoOverwrite_Mode;

        public static KeyBindingDef Planning_TogglePlanVisibility;

        public static KeyBindingDef Planning_ChangeShapeVariant;

        public static KeyBindingDef Planning_Action1;

        public static KeyBindingDef Planning_Action2;
        
        public static KeyBindingDef Planning_Undo;
        
        public static KeyBindingDef Planning_Redo;
        
        static PlanningKeyBindingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PlanningKeyBindingDefOf));
        }
    }
}
