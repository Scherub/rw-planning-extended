using System.Collections.Generic;
using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Utilities;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details
{
    internal class PlanChangeTextureSetButton<TDesignator> : BasePlanDetailButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        public PlanChangeTextureSetButton(GridPosition? gridPosition = null)
            : base("UI/Designators/ChangePlanAppearance", gridPosition)
        {
            OnClick = OpenAppearanceSelectionDialog;
        }

        void OpenAppearanceSelectionDialog()
        {
            List<FloatMenuOption> list = GuiMenuOptionsUtilities.GetTextureSetsMenuOptions(PlanDesignationType);

            Find.WindowStack.Add(new FloatMenu(list));
        }
    }
}
