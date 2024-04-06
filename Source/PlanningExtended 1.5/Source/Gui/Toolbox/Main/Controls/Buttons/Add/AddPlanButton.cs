using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators;
using PlanningExtended.Plans.Appearances;
using UnityEngine;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Plan.Add
{
    internal class AddPlanButton<TDesignator> : DesignatorButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        public AddPlanButton(GridPosition? gridPosition = null)
            : base(gridPosition)
        {
            PlanAppearanceManager.TextureSetChanged += TextureSetChanged;
        }

        void TextureSetChanged(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == Designator.PlanDesignationType)
                Icon.Texture = (Texture2D)Designator.icon;
        }
    }
}
