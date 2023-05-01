using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Shapes;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Designators.Shapes.ExtraControls
{
    internal class DisplayShapeVariantExtraControlWidget : BaseShapeExtraControlWidget
    {
        readonly LayoutGrid _layoutGrid = new(
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f) },
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Flexible(1f) },
            Thickness.Symmetric(0f, 5f));

        public override ShapeOptions ShapeOption => ShapeOptions.None;

        public override ShapeDisplayOptions ShapeDisplayOption => ShapeDisplayOptions.DisplayVariant;

        public override float RequiredHeight => 60f;

        protected override void OnDraw(Rect widgetRect, BaseShape shape)
        {
            _layoutGrid.Compute(widgetRect);

            Text.Anchor = TextAnchor.MiddleCenter;

            Rect cellRect = _layoutGrid.GetRect(0, 0);
            Widgets.Label(cellRect, $"{"PlanningExtended.Shapes.ChangeShapeVariant".Translate()}: {PlanningKeyBindingDefOf.Planning_ChangeShapeVariant.MainKeyLabel}");

            cellRect = _layoutGrid.GetRect(0, 1);
            Widgets.Label(cellRect, $"PlanningExtended.Shapes.Variants.{shape.SelectedShapeVariant.ShapeVariant}".Translate());

            //HandleChangeShapeVariant(shape);
        }

        //void HandleChangeShapeVariant(BaseShape shape)
        //{
        //    shape.ChangeToNextShapeVariant();
        //}
    }
}
