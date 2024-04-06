using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Panels
{
    internal class ToolBarPanel : GridPanel
    {
        public ToolBarPanel(GridPosition? gridPosition = null)
            : base("48, 48, 8, 48, 48, 48, 8, 48, 48", "*", gridPosition, Thickness.Symmetric(8f, 0f), 4f, 0f)
        {
            Children.Add(new DesignatorClickButton<LoadPlanDesignator>(GridPosition.StartIndex(0, 0)));
            Children.Add(new DesignatorClickButton<SavePlanDesignator>(GridPosition.StartIndex(1, 0)));

            Children.Add(new DesignatorButton<CutPlanDesignator>(GridPosition.StartIndex(3, 0)));
            Children.Add(new DesignatorButton<CopyPlanDesignator>(GridPosition.StartIndex(4, 0)));
            Children.Add(new DesignatorButton<PastePlanDesignator>(GridPosition.StartIndex(5, 0)));

            Children.Add(new DesignatorClickButton<UndoPlanDesignator>(GridPosition.StartIndex(7, 0)));
            Children.Add(new DesignatorClickButton<RedoPlanDesignator>(GridPosition.StartIndex(8, 0)));
        }
    }
}
