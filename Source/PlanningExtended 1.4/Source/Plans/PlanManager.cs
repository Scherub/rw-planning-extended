using System;

namespace PlanningExtended.Plans
{
    public static class PlanManager
    {
        public static PlanLayout CachedPlanLayout { get; private set; }

        public static bool ArePlansVisible { get; private set; } = true;

        public static event Action<PlanLayout> OnCachedPlanLayoutChanged;

        public static void SetCachedPlanLayout(PlanLayout planLayout)
        {
            CachedPlanLayout = planLayout;

            OnCachedPlanLayoutChanged?.Invoke(planLayout);
        }

        public static void SetArePlansVisible(bool isVisible)
        {
            ArePlansVisible = isVisible;
        }
    }
}
