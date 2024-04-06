using PlanningExtended.Designators;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Paint
{
    internal class PaintPlanButton : DesignatorButton<PaintPlanDesignator>
    {
        public PaintPlanButton(GridPosition? gridPosition = null)
            : base(gridPosition)
        {
            Icon.Scale = 0.85f;
        }

        protected override void OnDraw(Rect rect)
        {
            base.OnDraw(rect);

            Widgets.DrawTextureFitted(rect, Designator.IconTopTexture, 0.85f);
        }
    }
}
