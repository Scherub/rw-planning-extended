using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Plan.Add;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Panels
{
    internal class PlanButtonPanel<TDesignator> : GridPanel
        where TDesignator : BaseAddPlanDesignator
    {
        public PlanButtonPanel(GridPosition gridPosition)
            : base("*, *", "2*, 1*, 1*", gridPosition)
            //: base("*,*,*,*", "4*,1*", gridPosition)
        {
            Children.Add(new AddPlanButton<TDesignator>(new GridPosition(0, 0, 2, 1)));
            Children.Add(new PlanChangeColorButton<TDesignator>(GridPosition.StartIndex(0, 1)));
            Children.Add(new PlanToggleVisibilityButton<TDesignator>(GridPosition.StartIndex(1, 1)));
            Children.Add(new PlanChangeOpacityButton<TDesignator>(GridPosition.StartIndex(0, 2)));
            Children.Add(new PlanChangeTextureSetButton<TDesignator>(GridPosition.StartIndex(1, 2)));
        }
    }
}
