using System.Collections.Generic;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Shapes;
using PlanningExtended.Shapes.Features;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Designators.Shapes.ExtraControls
{
    internal class ShapeRotationExtraControlWidget : BaseShapeExtraControlWidget
    {
        readonly LayoutGrid _layoutGrid = new(
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Fixed(64f), TrackDefinition.Fixed(10f), TrackDefinition.Fixed(64f), TrackDefinition.Flexible(1f) },
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Fixed(64f), TrackDefinition.Flexible(1f), TrackDefinition.Fixed(30f) },
            Thickness.Symmetric(10f, 5f), 0f, 5f);

        public ShapeRotationExtraControlWidget()
            : base("*, 64, 10, 64, *", "*, 64, *, 30", margin: Thickness.Symmetric(10f, 5f), rowGap: 5f)
        {
        }

        public override ShapeDisplayOptions ShapeDisplayOption => ShapeDisplayOptions.Rotation;

        public override ShapeOptions ShapeOption => ShapeOptions.Rotation;

        public override float RequiredHeight => 120f;

        protected override void OnDraw(Rect widgetRect, BaseShape shape)
        {
            if (!shape.SelectedShapeVariant.ShapeFeatureManager.HasRotationFeature)
            {
                Text.Anchor = TextAnchor.MiddleCenter;

                Widgets.Label(widgetRect, "No rotation available");

                return;
            }

            RotationShapeFeature rotationShapeFeature = shape.SelectedShapeVariant.ShapeFeatureManager.RotationShapeFeature;

            _layoutGrid.Compute(widgetRect);

            Text.Anchor = TextAnchor.MiddleCenter;

            Widgets.Label(_layoutGrid.GetRect(0, 3, 5), "PlanningExtended.Direction".Translate() + ": " + $"PlanningExtended.Direction.{rotationShapeFeature.Rotation}".Translate());

            Text.Font = GameFont.Medium;

            ShapeOptionDirection shapeOptionDirection = ShapeOptionDirection.None;

            shapeOptionDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(1, 1), Textures.RotateLeft, rotationShapeFeature.KeyBindingLeft.MainKeyLabel, shapeOptionDirection, () => ShapeOptionDirection.Left);
            shapeOptionDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(3, 1), Textures.RotateRight, rotationShapeFeature.KeyBindingRight.MainKeyLabel, shapeOptionDirection, () => ShapeOptionDirection.Right);

            HandleShapeOptionDirection(shape.SelectedShapeVariant, shapeOptionDirection);
        }
    }
}
