using System.Collections.Generic;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Shapes;
using PlanningExtended.Shapes.Variants;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Designators.Shapes.ExtraControls
{
    internal class ShapeSegmentExtraControlWidget : BaseShapeExtraControlWidget
    {
        readonly LayoutGrid _layoutGrid = new(
            new List<TrackDefinition>() { TrackDefinition.Fixed(10f), TrackDefinition.Fixed(20f), TrackDefinition.Flexible(1f), TrackDefinition.Fixed(20f), TrackDefinition.Fixed(20f), TrackDefinition.Fixed(20f), TrackDefinition.Fixed(20f) },
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Flexible(1f), TrackDefinition.Flexible(1f) },
            Thickness.Symmetric(10f, 5f), 0f, 5f);

        public override ShapeOptions ShapeOption => ShapeOptions.NumberOfSegmentsX;

        public override ShapeDisplayOptions ShapeDisplayOption => ShapeDisplayOptions.NumberOfSegments;

        public override float RequiredHeight => 100f;

        protected override void OnDraw(Rect widgetRect, BaseShape shape)
        {
            if (shape.SelectedShapeVariant is not IShapeSegmentVariant shapeSegment)
            {
                Text.Anchor = TextAnchor.MiddleCenter;

                Widgets.Label(widgetRect, "No segments available");

                return;
            }

            _layoutGrid.Compute(widgetRect);

            Text.Anchor = TextAnchor.MiddleCenter;

            Widgets.Label(_layoutGrid.GetRect(0, 0, 7, 1), "PlanningExtended.Shapes.NumberOf".Translate(GetNumberOfTranslation(shape.SelectedShapeVariant)));

            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.Label(_layoutGrid.GetRect(2, 1), "PlanningExtended.Shapes.Columns".Translate());
            Widgets.Label(_layoutGrid.GetRect(2, 2), "PlanningExtended.Shapes.Rows".Translate());

            Widgets.Label(_layoutGrid.GetRect(1, 1), shapeSegment.NumberOfColumns.ToString());
            Widgets.Label(_layoutGrid.GetRect(1, 2), shapeSegment.NumberOfRows.ToString());

            Widgets.Label(_layoutGrid.GetRect(4, 1), KeyBindingDefOf.Designator_RotateLeft.MainKeyLabel);
            Widgets.Label(_layoutGrid.GetRect(6, 1), KeyBindingDefOf.Designator_RotateRight.MainKeyLabel);

            Widgets.Label(_layoutGrid.GetRect(4, 2), PlanningKeyBindingDefOf.Planning_Action1.MainKeyLabel);
            Widgets.Label(_layoutGrid.GetRect(6, 2), PlanningKeyBindingDefOf.Planning_Action2.MainKeyLabel);

            Text.Anchor = TextAnchor.MiddleCenter;

            Widgets.Label(_layoutGrid.GetRect(3, 1), "-");
            Widgets.Label(_layoutGrid.GetRect(3, 2), "-");

            Widgets.Label(_layoutGrid.GetRect(5, 1), "+");
            Widgets.Label(_layoutGrid.GetRect(5, 2), "+");

            ShapeOptionDirection shapeOptionDirection = ShapeOptionDirection.None;

            HandleShapeOptionDirection(shape.SelectedShapeVariant, shapeOptionDirection);
        }

        string GetNumberOfTranslation(BaseShapeVariant shapeVariant)
        {
            if (shapeVariant.ShapeVariant == ShapeVariant.Points)
                return "PlanningExtended.Shapes.Variants.Points".Translate();
            else
                return "PlanningExtended.Shapes.Cells".Translate();
        }
    }
}
