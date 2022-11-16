using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse.Sound;

namespace PlanningExtended.Plans
{
    public static class PlanManager
    {
        readonly static List<PlanConfig> planConfigs;

        readonly static PlanConfig planDoor = new(PlanDesignationType.PlanDoors);

        readonly static PlanConfig planFloor = new(PlanDesignationType.PlanFloors);

        readonly static PlanConfig planObject = new(PlanDesignationType.PlanObjects);

        readonly static PlanConfig planWall = new(PlanDesignationType.PlanWall);

        public static PlanLayout CachedPlanLayout { get; private set; }

        public static PlanVisibility PlanVisibility { get; private set; }

        public static bool ArePlansVisible => PlanVisibility is PlanVisibility.Visible;

        public static event Action<PlanLayout> OnCachedPlanLayoutChanged;

        public static event Action<PlanVisibility> OnPlanVisibilityChanged;

        static PlanManager()
        {
            planConfigs = new[] { planDoor, planFloor, planObject, planWall }.ToList();

            DeterminePlanVisibility();
        }

        public static void SetCachedPlanLayout(PlanLayout planLayout)
        {
            CachedPlanLayout = planLayout;

            OnCachedPlanLayoutChanged?.Invoke(planLayout);
        }

        public static void SetIsPlanVisible(bool isVisible)
        {
            SetIsPlanVisible(PlanDesignationType.Unknown, isVisible);
        }

        public static void SetIsPlanVisible(PlanDesignationType planDesignationType, bool isVisible)
        {
            if (isVisible == IsPlanVisible(planDesignationType))
                return;

            foreach (PlanConfig planConfig in planConfigs)
                if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planConfig.Type)
                    planConfig.IsVisible = isVisible;

            DeterminePlanVisibility();

            OnPlanVisibilityChanged?.Invoke(PlanVisibility);
        }

        public static void ToggleIsPlanVisible(PlanDesignationType planDesignationType)
        {
            bool isVisible = !IsPlanVisible(planDesignationType);

            PlayPlanVisibilityOnOffSound(isVisible);

            SetIsPlanVisible(planDesignationType, isVisible);
        }

        public static bool IsPlanVisible(PlanDesignationType planDesignationType)
        {
            return planDesignationType switch
            {
                PlanDesignationType.PlanDoors => planDoor.IsVisible,
                PlanDesignationType.PlanFloors => planFloor.IsVisible,
                PlanDesignationType.PlanObjects => planObject.IsVisible,
                PlanDesignationType.PlanWall => planWall.IsVisible,
                _ => ArePlansVisible,
            };
        }

        static void DeterminePlanVisibility()
        {
            if (planConfigs.All(p => p.IsVisible))
                PlanVisibility = PlanVisibility.Visible;
            else if (planConfigs.Any(p => p.IsVisible))
                PlanVisibility = PlanVisibility.PartiallyVisible;
            else
                PlanVisibility = PlanVisibility.Invisible;
        }

        static void PlayPlanVisibilityOnOffSound(bool isVisible)
        {
            if (isVisible)
                SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
            else
                SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
        }

        public class PlanConfig
        {
            public PlanDesignationType Type { get; }

            public bool IsVisible { get; set; }

            public PlanConfig(PlanDesignationType type, bool isVisible = true)
            {
                Type = type;
                IsVisible = isVisible;
            }
        }
    }
}
