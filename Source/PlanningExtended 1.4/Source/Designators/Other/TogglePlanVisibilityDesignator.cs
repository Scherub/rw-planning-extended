using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Designators
{
    public class TogglePlanVisibilityDesignator : BaseClickDesignator
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
            PlanManager.SetArePlansVisible(!PlanManager.ArePlansVisible);

            if (PlanManager.ArePlansVisible)
                SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
            else
                SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
        }

        void PlanManager_OnPlanVisibilityChanged(bool isVisible)
        {
            icon = isVisible ? icon_on : icon_off;
        }
    }
}
