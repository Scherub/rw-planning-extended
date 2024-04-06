using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class HorizontalLine : BaseControl
    {
        public Color? Color { get; set; }

        public float Thickness { get; set; }

        public HorizontalLine(Color? color = null, float thickness = 1f, GridPosition? gridPosition = null)
            : base(gridPosition)
        {
            Color = color;
            Thickness = thickness;
        }

        protected override Size OnMeasure(Size availableSize)
        {
            return new Size(availableSize.Width, Thickness);
        }

        protected override void OnDraw(Rect rect)
        {
            if (Color != null)
                GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, Thickness), BaseContent.WhiteTex, ScaleMode.ScaleToFit, true, 0f, Color.Value, 0f, 0f);
            else
                GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, Thickness), BaseContent.WhiteTex);
        }
    }
}
