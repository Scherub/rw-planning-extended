using System;
using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal abstract class BaseButton : ContentControl
    {
        bool ShowBorder { get; set; }

        protected Action OnClick { get; set; }

        protected Color BorderColor { get; set; } = new(0.184f, 0.447f, 0.76f);

        protected Color BorderColorHover { get; set; } = new(0.284f, 0.547f, 0.86f);

        protected BaseButton(BaseControl child, GridPosition? gridPosition = null, Thickness? margin = null, bool showBorder = true, Action onClick = null)
            : base(child, gridPosition, margin)
        {
            ShowBorder = showBorder;
            OnClick = onClick;
        }

        protected override void OnDraw(Rect rect)
        {
            Material material = IsEnabled ? null : TexUI.GrayscaleGUI;

            GenUI.DrawTextureWithMaterial(rect, Command.BGTex, material);

            if (ShowBorder && IsEnabled)
            {
                Color oldColor = GUI.color;
                
                GUI.color = Mouse.IsOver(rect) ? BorderColorHover : BorderColor;

                Widgets.DrawBox(rect);

                GUI.color = oldColor;
            }
        }
    }
}
