using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons
{
    internal class BasePlanDetailButton<TDesignator> : BasePlanButton<TDesignator>
        where TDesignator : BaseAddPlanDesignator
    {
        protected PlanDesignationType PlanDesignationType { get; }

        public BasePlanDetailButton(string texturePath, GridPosition? gridPosition = null)
            : base(texturePath, gridPosition)
        {
            PlanDesignationType = Designator.PlanDesignationType;
        }
    }
}
