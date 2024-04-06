using System.Collections.Generic;
using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Utilities;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details
{
    internal class PlanChangeOpacityButton<TDesignator> : BasePlanDetailButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        public PlanChangeOpacityButton(GridPosition? gridPosition = null)
            : base("UI/Designators/TogglePlanVisibility_Partial", gridPosition)
        {
            OnClick = OpenOpacitySelectionDialog;
        }

        void OpenOpacitySelectionDialog()
        {
            List<FloatMenuOption> list = GuiMenuOptionsUtilities.GetOpacityMenuOptions(PlanDesignationType);

            Find.WindowStack.Add(new FloatMenu(list));
        }
    }
}
