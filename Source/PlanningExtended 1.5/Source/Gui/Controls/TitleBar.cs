using System;
using PlanningExtended.Gui.Controls.Grid;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class TitleBar : GridPanel
    {
        public string Title { get; }

        public TitleBar(string title, GridPosition? gridPosition = null, float buttonSize = 24f, bool showCloseButton = true, Action onCloseClick = null)
            : base($"*, {buttonSize}", $"{buttonSize}", gridPosition, Thickness.Only(left: 5f))
        {
            Title = title;

            Children.Add(new Label(Title));

            if (showCloseButton)
                Children.Add(new IconButton(TexButton.CloseXSmall, gridPosition: GridPosition.StartIndex(1, 0), showBorder: false, onClick: onCloseClick));
        }
    }
}
