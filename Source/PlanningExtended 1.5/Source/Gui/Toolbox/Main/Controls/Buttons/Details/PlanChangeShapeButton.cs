using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Details
{
    internal class PlanChangeShapeButton<TDesignator> : BasePlanDetailButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        public PlanChangeShapeButton(GridPosition? gridPosition = null)
            : base("UI/Designators/ChangePlanAppearance", gridPosition)
        {
        }
    }
}
