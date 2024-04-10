using PlanningExtended.Updates;
using Verse;

namespace PlanningExtended.Plans.Maps
{
    public class PlanMapComponent : MapComponent
    {
        int planVersion;

        public PlanMapComponent(Map map)
            : base(map)
        {
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref planVersion, nameof(planVersion), 0);
        }

        public override void FinalizeInit()
        {
            planVersion = PlanUpdateManager.ApplyUpdates(map, planVersion);
        }
    }
}
