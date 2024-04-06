using System.Collections.Generic;
using PlanningExtended.Gui.Controls.Grid;

namespace PlanningExtended.Gui.Controls
{
    internal abstract class Panel : BaseControl
    {
        public List<BaseControl> Children { get; } = new();

        protected Panel(GridPosition? gridPosition, Thickness? margin)
            : base(gridPosition, margin)
        {
        }
    }
}
