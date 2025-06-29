using System;

namespace PlanningExtended.Plans
{
    public static class PlanManager
    {
        public static PlanLayout CachedPlanLayout { get; private set; }

        public static event Action<PlanLayout> OnCachedPlanLayoutChanged;

        static PlanManager()
        {
        }

        public static void SetCachedPlanLayout(PlanLayout planLayout)
        {
            CachedPlanLayout = planLayout;

            OnCachedPlanLayoutChanged?.Invoke(planLayout);
        }
    }
}
