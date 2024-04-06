using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Shapes;
using PlanningExtended.Shapes.Variants;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Gui.Designators.Shapes.ExtraControls
{
    internal abstract class BaseShapeExtraControlWidget : GridPanel
    {
        public abstract ShapeDisplayOptions ShapeDisplayOption { get; }

        public abstract ShapeOptions ShapeOption { get; }

        public abstract float RequiredHeight { get; }

        protected BaseShapeExtraControlWidget(string columnDefinitions, string rowDefinitions, GridPosition? gridPosition = null, Thickness? margin = null, float columnGap = 0, float rowGap = 0)
            : base(columnDefinitions, rowDefinitions, gridPosition, margin, columnGap, rowGap)
        {
        }

        public void Draw(Rect widgetRect, BaseShape shape)
        {
            if (shape.SelectedShapeVariant == null)
                return;

            TextAnchor originalAnchor = Text.Anchor;
            GameFont originalFont = Text.Font;

            OnDraw(widgetRect, shape);

            Text.Anchor = originalAnchor;
            Text.Font = originalFont;
        }

        protected abstract void OnDraw(Rect widgetRect, BaseShape shape);

        protected void HandleShapeOptionDirection(BaseShapeVariant shapeVariant, ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptionDirection != ShapeOptionDirection.None)
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);

                shapeVariant?.ShapeFeatureManager.ChangeShapeOption(ShapeOption, shapeOptionDirection);
            }
        }
    }
}
