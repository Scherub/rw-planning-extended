using Verse;

namespace PlanningExtended.Settings
{
    public abstract class BaseSettings : IExposable
    {
        protected PlanningSettings PlanningSettings { get; }

        protected BaseSettings(PlanningSettings planningSettings)
        {
            PlanningSettings = planningSettings;
        }

        public abstract void ExposeData();

        public virtual void Reset()
        {
        }
    }
}
