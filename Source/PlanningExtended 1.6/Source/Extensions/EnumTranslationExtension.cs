using System;
using Verse;

namespace PlanningExtended;

internal static class EnumTranslationExtension
{
    public static string Translate<T>(this T value) where T : Enum
    {
        return $"PlanningExtended.{typeof(T).Name}.{value}".Translate();
    }

    public static string Translate<T>(this T value, string prefix) where T : Enum
    {
        return $"PlanningExtended.{prefix}.{value}".Translate();
    }

    public static string Translate(this StartupPlanVisibility startupPlanVisibility)
    {
        return startupPlanVisibility.Translate("StartupPlanVisibilities");
    }

    public static string Translate(this PlanGrabbingPosition planGrabbingPosition)
    {
        return planGrabbingPosition.Translate(nameof(PlanGrabbingPosition));
    }
}
