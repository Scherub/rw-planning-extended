using System.Collections.Generic;
using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Utilities;
using PlanningExtended.Plans.Appearances;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details
{
    internal class PlanChangeColorButton<TDesignator> : BasePlanDetailButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        public PlanChangeColorButton(GridPosition? gridPosition = null)
            : base("UI/Designators/ColorPicker", gridPosition)
        {
            OnClick = OpenColorSelectorDialog;
        }

        void OpenColorSelectorDialog()
        {
            List<FloatMenuGridOption> list = GuiMenuOptionsUtilities.GetColorSelectionMenuGridOptions(Designator.ColorPicker, (colorDef) => PlanAppearanceManager.SetPlanColor(PlanDesignationType, colorDef));

            Find.WindowStack.Add(new FloatMenuGrid(list));
        }
    }
}
