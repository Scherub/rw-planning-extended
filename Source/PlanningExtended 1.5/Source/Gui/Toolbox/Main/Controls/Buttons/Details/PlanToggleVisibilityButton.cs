using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Plans.Appearances;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details
{
    internal class PlanToggleVisibilityButton<TDesignator> : BasePlanDetailButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        readonly Texture2D _icon_on, _icon_off;

        public PlanToggleVisibilityButton(GridPosition? gridPosition = null)
            : base("UI/Designators/TogglePlanVisibility_On", gridPosition)
        {
            _icon_on = ContentFinder<Texture2D>.Get($"UI/Designators/TogglePlanVisibility_On", true);
            _icon_off = ContentFinder<Texture2D>.Get($"UI/Designators/TogglePlanVisibility_Off", true);
            OnClick = () => ToggleVisibility();

            IconTexture = GetIconTexture(PlanAppearanceManager.GetPlanVisibility(PlanDesignationType));

            PlanAppearanceManager.VisibilityChanged += PlanAppearanceManager_VisibilityChanged;
        }

        void ToggleVisibility()
        {
            PlanAppearanceManager.TogglePlanVisibility(PlanDesignationType);
        }

        Texture2D GetIconTexture(bool isVisible)
        {
            return isVisible switch
            {
                true => _icon_on,
                false => _icon_off,
            };
        }

        void PlanAppearanceManager_VisibilityChanged(PlanDesignationType planDesignationType, bool isVisible)
        {
            Log.Warning($"PlanToggleVisibilityButton '{PlanDesignationType}': Visibility changed to '{isVisible}' for '{planDesignationType}'");

            if (planDesignationType != PlanDesignationType.Unknown && planDesignationType != PlanDesignationType)
                return;
            
            IconTexture = GetIconTexture(isVisible);
        }
    }
}
