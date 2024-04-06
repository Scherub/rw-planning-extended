using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class Label : BaseControl
    {
        public string Text { get; set; }

        public TextAnchor Anchor { get; set; }

        public GameFont Font { get; set; }

        public Label(string text, GridPosition? gridPosition = null, TextAnchor anchor = TextAnchor.MiddleLeft, GameFont font = GameFont.Small)
             : base(gridPosition)
        {
            Text = text;
            Anchor = anchor;
            Font = font;
        }

        protected override Size OnMeasure(Size availableSize)
        {


            return base.OnMeasure(availableSize);
        }

        protected override void OnDraw(Rect rect)
        {
            Widgets.Label(rect, Text);
        }
    }
}
