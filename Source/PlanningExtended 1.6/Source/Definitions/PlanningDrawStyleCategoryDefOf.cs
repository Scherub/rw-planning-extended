using RimWorld;
using Verse;

namespace PlanningExtended;

[DefOf]
public static class PlanningDrawStyleCategoryDefOf
{
    public static DrawStyleCategoryDef PlanningExtended_Shape;

    static PlanningDrawStyleCategoryDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PlanningDrawStyleCategoryDefOf));
    }
}
