using Verse;

namespace PlanningExtended;

internal static class StartupPlanVisibilityExtensions
{
    public static string Translate(this StartupPlanVisibility startupPlanVisibility)
    {
        return (startupPlanVisibility switch
        {
            StartupPlanVisibility.Invisible => "PlanningExtended.Settings.StartupPlanVisibilities.Invisible",
            StartupPlanVisibility.LastSaved => "PlanningExtended.Settings.StartupPlanVisibilities.LastSaved",
            _ => "PlanningExtended.Settings.StartupPlanVisibilities.Visible"
        }).Translate();
    }
}
