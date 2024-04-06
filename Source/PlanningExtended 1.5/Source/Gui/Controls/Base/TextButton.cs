using System;
using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class TextButton : BaseButton
    {
        public string Text
        {
            get { return ((Label)Child).Text; }
            set { ((Label)Child).Text = value; }
        }

        public TextButton(string text, GridPosition? gridPosition = null, Thickness? margin = null, bool showBorder = true, Action onClick = null)
                : base(new Label(text, anchor: TextAnchor.MiddleCenter), gridPosition, margin, showBorder, onClick)
        {
        }
        
        protected override void OnDraw(Rect rect)
        {
            base.OnDraw(rect);

            Child.Draw(rect);

            if (IsEnabled && Widgets.ButtonInvisible(rect))
                OnClick?.Invoke();
        }
    }
}
