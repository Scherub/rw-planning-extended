using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Paint;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Panels
{
    internal class PlanButtonsToolBarPanel : GridPanel
    {
        const int ButtonSize = 64;

        public PlanButtonsToolBarPanel(GridPosition? gridPosition = null)
            : base($"{ButtonSize}, {ButtonSize}, {ButtonSize}, {ButtonSize}, {ButtonSize}, {ButtonSize}", "*, 64", gridPosition, Thickness.Symmetric(8f, 0f), 8f, 0f)
        {
            Children.Add(new PlanButtonPanel<PlanWallsDesignator>(new GridPosition(0, 0, 1, 2)));
            Children.Add(new PlanButtonPanel<PlanDoorsDesignator>(new GridPosition(1, 0, 1, 2)));
            Children.Add(new PlanButtonPanel<PlanObjectsDesignator>(new GridPosition(2, 0, 1, 2)));
            Children.Add(new PlanButtonPanel<PlanFloorsDesignator>(new GridPosition(3, 0, 1, 2)));
            Children.Add(new PaintPlanButton(GridPosition.StartIndex(4, 0)));
            Children.Add(new DesignatorButton<RemovePlanDesignator>(GridPosition.StartIndex(5, 0)));
        }
    }
}
