using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Panels
{
    internal class TitleBarPanel : GridPanel
    {
        public TitleBarPanel(GridPosition? gridPosition = null)
            : base("*, 24", "24", gridPosition: gridPosition, margin: Thickness.Only(left: 5f))
        {
            Children.Add(new Label("Planning Toolbox", gridPosition: GridPosition.StartIndex(0, 0)));
            Children.Add(new IconButton(TexButton.CloseXSmall, gridPosition: GridPosition.StartIndex(1, 0), showBorder: false, onClick: () => ToolboxManager.CloseToolboxWindow()));
        }
    }
}
