using PlanningExtended.Defs;
using Verse;

namespace PlanningExtended
{
    [StaticConstructorOnStartup]
    public static class PlanningModStartUp
    {
        static PlanningModStartUp()
        {
            DefsUpdater.UpdateDefs();
        }
    }
}
