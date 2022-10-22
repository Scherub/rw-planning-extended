using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Gui;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Designators
{
    public class PastePlanDesignator : BaseUndoRedoPlanDesignator
    {
        PlanLayout _selectedPlanLayout = null;

        public override int DraggableDimensions => 0;

        public override bool DragDrawMeasurements => false;

        public PastePlanDesignator()
            : base("PastePlan")
        {
            disabled = true;

            PlanManager.OnCachedPlanLayoutChanged += PlanManager_OnCachedPlanLayoutChanged;
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

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            //return base.CanDesignateCell(loc);
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (_selectedPlanLayout == null)
                return;

            AreaDimensions areaDimensions = _selectedPlanLayout.Dimensions + c;

            PlanLayout undoPlanLayout = CreateUndoPlanLayout(areaDimensions);

            PlanLayoutUtilities.Designate(_selectedPlanLayout, c, Map);

            CreateRedoPlanLayout(undoPlanLayout);
        }

        //public override void RenderHighlight(List<IntVec3> dragCells)
        //{
        //    base.RenderHighlight(dragCells);
        //}

        public override void SelectedUpdate()
        {
            //base.SelectedUpdate();

            //GenDraw.DrawNoBuildEdgeLines();
            //GenDraw.DrawFieldEdges(_selectedPlanLayout.Cells.Select(c => c.Position.ToIntVec3).ToList());

            PlanLayoutUtilities.Draw(_selectedPlanLayout, UI.MouseCell(), Map);
        }

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
                Text.Anchor = TextAnchor.MiddleCenter;
                Text.Font = GameFont.Medium;

                RotationDirection rotationDirection = RotationDirection.None;
                FlipDirection flipDirection = FlipDirection.None;

                Rect rect = new(winRect.width / 2f - 64f - 5f, 15f, 64f, 64f);

                rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, TexUI.RotLeftTex, KeyBindingDefOf.Designator_RotateLeft.MainKeyLabel, RotationDirection.Counterclockwise, rotationDirection);

                rect = new(winRect.width / 2f + 5f, 15f, 64f, 64f);

                rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, TexUI.RotRightTex, KeyBindingDefOf.Designator_RotateRight.MainKeyLabel, RotationDirection.Clockwise, rotationDirection);

                rect = new(winRect.width / 2f - 64f - 5f, winRect.height / 2f + 15f, 64f, 64f);

                flipDirection = GuiUtilities.DrawButtonImageFlip(rect, TexUI.RotRightTex, PlanningKeyBindingDefOf.Planning_FlipHorizontal.MainKeyLabel, FlipDirection.Horizontally, flipDirection);

                rect = new(winRect.width / 2f + 5f, winRect.height / 2f + 15f, 64f, 64f);

                flipDirection = GuiUtilities.DrawButtonImageFlip(rect, TexUI.RotLeftTex, PlanningKeyBindingDefOf.Planning_FlipVertical.MainKeyLabel, FlipDirection.Vertically, flipDirection);

                Text.Anchor = TextAnchor.UpperLeft;
                Text.Font = GameFont.Small;

                HandleRotationFlip(rotationDirection, flipDirection);
            }, true, false, 1f, null);
        }

        void HandleShortcuts()
        {
            RotationDirection rotationDirection = RotationDirection.None;
            FlipDirection flipDirection = FlipDirection.None;

            if (KeyBindingDefOf.Designator_RotateRight.KeyDownEvent)
                rotationDirection = RotationDirection.Clockwise;
            if (KeyBindingDefOf.Designator_RotateLeft.KeyDownEvent)
                rotationDirection = RotationDirection.Counterclockwise;

            if (PlanningKeyBindingDefOf.Planning_FlipHorizontal.KeyDownEvent)
                flipDirection = FlipDirection.Horizontally;
            if (PlanningKeyBindingDefOf.Planning_FlipVertical.KeyDownEvent)
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
