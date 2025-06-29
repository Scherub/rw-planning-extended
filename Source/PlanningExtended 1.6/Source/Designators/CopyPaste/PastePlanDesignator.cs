using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Gui;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Designators
{
    public class PastePlanDesignator : BaseUndoRedoPlanDesignator
    {
        readonly LayoutGrid _layoutGrid = new(
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Fixed(64f), TrackDefinition.Fixed(10f), TrackDefinition.Fixed(64f), TrackDefinition.Flexible(1f) },
            new List<TrackDefinition>() { TrackDefinition.Flexible(1f), TrackDefinition.Fixed(64f), TrackDefinition.Flexible(1f), TrackDefinition.Fixed(64f), TrackDefinition.Flexible(1f) },
            Thickness.Symmetric(10f, 0f), 0f, 0f);

        PlanLayout _selectedPlanLayout = null;

        public override bool DragDrawMeasurements => false;

        protected override bool DrawMouseOverBrackets => false;

        public PastePlanDesignator()
            : base("PastePlan")
        {
            disabled = true;

            Plans.PlanManager.OnCachedPlanLayoutChanged += PlanManager_OnCachedPlanLayoutChanged;
        }

        public override void ProcessInput(Event ev)
        {
            if (_selectedPlanLayout == null)
                return;

            base.ProcessInput(ev);
        }

        public override void SelectedProcessInput(Event ev)
        {
            HandleShortcuts();
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();

            //GenDraw.DrawFieldEdges(_selectedPlanLayout.Cells.Select(c => c.Position.ToIntVec3).ToList());

            PlanLayoutUtilities.Draw(Map, _selectedPlanLayout, UI.MouseCell(), OverwriteDesignation);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (_selectedPlanLayout == null)
                return;

            AreaDimensions areaDimensions = _selectedPlanLayout.Dimensions + c;

            PlanLayout undoPlanLayout = CreateUndoPlanLayout(areaDimensions);

            PlanLayoutUtilities.Designate(Map, _selectedPlanLayout, c, OverwriteDesignation);

            CreateRedoPlanLayout(undoPlanLayout);
        }

        //public override void RenderHighlight(List<IntVec3> dragCells)
        //{
        //    base.RenderHighlight(dragCells);
        //}

        //public override void DrawPanelReadout(ref float curY, float width)
        //{
        //    if (ArchitectCategoryTab.InfoRect.Contains(UI.MousePositionOnUIInverted))
        //        return;

        //}

        public override void DoExtraGuiControls(float leftX, float bottomY)
        {
            float width = 200f;
            float height = 180f;

            Rect winRect = new(leftX, bottomY - height, width, height);

            Find.WindowStack.ImmediateWindow(73095, winRect, WindowLayer.GameUI, () =>
            {
                _layoutGrid.Compute(new Rect(0f, 0f, width, height));

                Text.Anchor = TextAnchor.MiddleCenter;
                Text.Font = GameFont.Medium;

                RotationDirection rotationDirection = RotationDirection.None;
                FlipDirection flipDirection = FlipDirection.None;

                rotationDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(1, 1), Textures.RotateLeft, KeyBindingDefOf.Designator_RotateLeft.MainKeyLabel, rotationDirection, () => RotationDirection.Counterclockwise);
                rotationDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(3, 1), Textures.RotateRight, KeyBindingDefOf.Designator_RotateRight.MainKeyLabel, rotationDirection, () => RotationDirection.Clockwise);

                flipDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(1, 3), Textures.FlipHorizontal, PlanningKeyBindingDefOf.Planning_Action1.MainKeyLabel, flipDirection, () => FlipDirection.Horizontally);
                flipDirection = GuiUtilities.DrawImageButton(_layoutGrid.GetRect(3, 3), Textures.FlipVertical, PlanningKeyBindingDefOf.Planning_Action2.MainKeyLabel, flipDirection, () => FlipDirection.Vertically);

                Text.Anchor = TextAnchor.UpperLeft;
                Text.Font = GameFont.Small;

                HandleRotationFlip(rotationDirection, flipDirection);
            });
        }

        protected override void OnSkipExistingDesignationsKeyChanged(bool isPressed)
        {
            ResetMouseAttachmentText();
        }

        protected override string GetMouseAttachmentText()
        {
            return $"{"PlanningExtended.Mode".Translate()}: {GetSkipReplaceModeString()}\n" + base.GetMouseAttachmentText();
        }

        void HandleShortcuts()
        {
            RotationDirection rotationDirection = RotationDirection.None;
            FlipDirection flipDirection = FlipDirection.None;

            if (KeyBindingDefOf.Designator_RotateRight.KeyDownEvent)
                rotationDirection = RotationDirection.Clockwise;
            if (KeyBindingDefOf.Designator_RotateLeft.KeyDownEvent)
                rotationDirection = RotationDirection.Counterclockwise;

            if (PlanningKeyBindingDefOf.Planning_Action1.KeyDownEvent)
                flipDirection = FlipDirection.Horizontally;
            if (PlanningKeyBindingDefOf.Planning_Action2.KeyDownEvent)
                flipDirection = FlipDirection.Vertically;

            HandleRotationFlip(rotationDirection, flipDirection);
        }

        void HandleRotationFlip(RotationDirection rotationDirection, FlipDirection flipDirection)
        {
            if (rotationDirection != RotationDirection.None)
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);

                _selectedPlanLayout = PlanLayoutUtilities.Rotate(_selectedPlanLayout, rotationDirection);
            }

            if (flipDirection != FlipDirection.None)
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);

                _selectedPlanLayout = PlanLayoutUtilities.Flip(_selectedPlanLayout, flipDirection);
            }
        }

        void PlanManager_OnCachedPlanLayoutChanged(PlanLayout planLayout)
        {
            disabled = false;
            _selectedPlanLayout = planLayout;

            Find.DesignatorManager.Select(this);
        }
    }
}
