using PlanningExtended.Shapes;
using PlanningExtended.Shapes.Variants;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Designators.Gui.Shapes.ExtraControls
{
    internal abstract class BaseShapeExtraControlWidget
    {
        public abstract ShapeOptions ShapeOption { get; }

        public abstract ShapeDisplayOptions ShapeDisplayOption { get; }

        public abstract float RequiredHeight { get; }

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

                shapeVariant?.ChangeShapeOption(ShapeOption, shapeOptionDirection);
            }
        }
    }
}
