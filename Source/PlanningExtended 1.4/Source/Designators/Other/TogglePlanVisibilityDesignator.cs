using System.Collections.Generic;
using PlanningExtended.Plans;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class TogglePlanVisibilityDesignator : BasePlanMenuDesignator
    {
        readonly Texture icon_on, icon_off, icon_partial;

        public override bool Visible => PlanningMod.Settings.displayTogglePlanVisibilityDesignator;

        public TogglePlanVisibilityDesignator()
            : base("TogglePlanVisibility", "TogglePlanVisibility_On")
        {
            icon_on = ContentFinder<Texture2D>.Get($"UI/Designators/TogglePlanVisibility_On", true);
            icon_off = ContentFinder<Texture2D>.Get($"UI/Designators/TogglePlanVisibility_Off", true);
            icon_partial = ContentFinder<Texture2D>.Get($"UI/Designators/TogglePlanVisibility_Partial", true);

            PlanManager.OnPlanVisibilityChanged += PlanManager_OnPlanVisibilityChanged;
        }

        public override void ProcessInput(Event ev)
        {
            if (IsPlanMenuKeyPressed)
            {
                List<FloatMenuOption> list = GetPlanTypeMenuOptions((planDesignationType) =>
                {
                    PlanManager.ToggleIsPlanVisible(planDesignationType);
                });

                Find.WindowStack.Add(new FloatMenu(list));
            }
            else
            {
                PlanManager.ToggleIsPlanVisible(PlanDesignationType.Unknown);
            }
        }

        void PlanManager_OnPlanVisibilityChanged(PlanVisibility planVisibility)
        {
            icon = planVisibility switch
            {
                PlanVisibility.Visible => icon_on,
                PlanVisibility.PartiallyVisible => icon_partial,
                _ => icon_off,
            };
        }
    }
}
