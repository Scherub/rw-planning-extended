using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators
{
    internal class DesignatorClickButton<TDesignator> : DesignatorButton<TDesignator>
        where TDesignator : BaseClickDesignator
    {
        public DesignatorClickButton(GridPosition? gridPosition = null, Thickness? margin = null)
            : base(gridPosition, margin)
        {
            OnClick = () => Designator.Click();
        }
    }
}
