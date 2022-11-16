using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using PlanningExtended.Gui;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseAddPlanDesignator : BaseColorPlanDesignator
    {
        protected abstract PlanDesignationType PlanDesignationType { get; }

        protected override bool HasLeftClickPopupMenu => true;

        public override Color IconDrawColor => colorDef != null ? colorDef.color : Color.white;

        protected BaseAddPlanDesignator(string name)
            : base(name)
        {
        }

        protected override bool DesignateMultiCellInternal(IEnumerable<IntVec3> cells)
        {
            CellArea cellArea = new(cells);
            AreaDimensions areaDimensions = cellArea.Dimensions;

            bool somethingSucceeded = false;
            bool flag = false;

            foreach (IntVec3 cell in cells)
            {
                if (IsShapeCellValid(cell, areaDimensions) && CanDesignateCell(cell).Accepted)
                {
                    DesignateSingleCell(cell);

                    somethingSucceeded = true;

                    if (!flag)
                        flag = ShowWarningForCell(cell);
                }
            }

            return somethingSucceeded;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (IsColorPickModeEnabled)
                return colorPicker.CanDesignateCell(c);

            if (!base.CanDesignateCell(c))
                return false;

            return !PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.IsDown || !Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (IsColorPickModeEnabled)
            {
                colorPicker.DesignateSingleCell(c);
                return;
            }

            Map.designationManager.RemovePlanDesignationsAt(c);

            //Log.Warning($"Designating: Cell ({c}), Designation ({Designation}), Color ({colorDef})");
            Map.designationManager.AddDesignation(new PlanDesignation(c, SelectedDesignation, colorDef));
        }

        public override void RenderHighlight(List<IntVec3> dragCells)
        {
            CellArea cellArea = new(dragCells);
            AreaDimensions areaDimensions = cellArea.Dimensions;

            List<IntVec3> cells = new();

            foreach (IntVec3 cell in dragCells)
                if (IsShapeCellValid(cell, areaDimensions))
                    cells.Add(cell);

            DesignatorUtility.RenderHighlightOverSelectableCells(this, cells);
        }

        //public override void DrawPanelReadout(ref float curY, float width)
        //{
        //    //base.DrawPanelReadout(ref curY, width);
        //    Widgets.InfoCardButton(width - 24f - 2f, 6f, this.entDef);

        //}

        public override void DoExtraGuiControls(float leftX, float bottomY)
        {
            float width = 200f;
            //float height = 180f;
            float height = 90f;

            float columnWidthTotal = width / 2f;
            float columnWidth = columnWidthTotal - 20f;
            float rowHeight = 64f;

            float marginLeft = (columnWidthTotal - columnWidth) / 2f;
            float marginTop = (height - rowHeight) / 2f;


            Rect winRect = new(leftX, bottomY - height, width, height);

            Find.WindowStack.ImmediateWindow(73095, winRect, WindowLayer.GameUI, () =>
            {
                Text.Anchor = TextAnchor.MiddleCenter;
                //Text.Font = GameFont.Medium;

                //RotationDirection rotationDirection = RotationDirection.None;
                //FlipDirection flipDirection = FlipDirection.None;

                //Rect rect = new(winRect.width / 2f - 64f - 5f, 15f, 64f, 64f);
                Rect rect = new(marginLeft, marginTop, columnWidth, rowHeight);

                Widgets.Label(rect, $"Change shape variant: {PlanningKeyBindingDefOf.Planning_ChangeShapeVariant.MainKeyLabel}");

                //rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, Textures.RotateLeft, KeyBindingDefOf.Designator_RotateLeft.MainKeyLabel, RotationDirection.Counterclockwise, rotationDirection);

                //rect = new(winRect.width / 2f + 5f, 15f, 64f, 64f);
                rect = new(columnWidthTotal + marginLeft, marginTop, columnWidth, rowHeight);

                Widgets.Label(rect, SelectedShape.SelectedShapeVariant.ShapeVariant.ToString());

                //rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, Textures.RotateRight, KeyBindingDefOf.Designator_RotateRight.MainKeyLabel, RotationDirection.Clockwise, rotationDirection);

                //rect = new(winRect.width / 2f - 64f - 5f, winRect.height / 2f + 15f, 64f, 64f);

                //flipDirection = GuiUtilities.DrawButtonImageFlip(rect, Textures.FlipHorizontal, PlanningKeyBindingDefOf.Planning_Action1.MainKeyLabel, FlipDirection.Horizontally, flipDirection);

                //rect = new(winRect.width / 2f + 5f, winRect.height / 2f + 15f, 64f, 64f);

                //flipDirection = GuiUtilities.DrawButtonImageFlip(rect, Textures.FlipVertical, PlanningKeyBindingDefOf.Planning_Action2.MainKeyLabel, FlipDirection.Vertically, flipDirection);

                Text.Anchor = TextAnchor.UpperLeft;
                Text.Font = GameFont.Small;

                //HandleRotationFlip(rotationDirection, flipDirection);
            }, true, false, 1f, null);
        }

        protected override void OnModifierKeyChanged(bool isPressed)
        {
            ResetMouseAttachmentText();
        }

        protected override string GetMouseAttachmentText()
        {
            string mode = IsModifierKeyPressed ? "PlanningExtended.Skip".Translate() : "PlanningExtended.Replace".Translate();

            return $"{"PlanningExtended.Mode".Translate()}: {mode}\n" + base.GetMouseAttachmentText();
        }

        protected override void SetColorDef(ColorDef newColorDef)
        {
            base.SetColorDef(newColorDef);

            PlanningMod.Settings.SetColor(PlanDesignationType, newColorDef.defName);
        }

        protected override ColorDef GetColorDef()
        {
            string color = PlanningMod.Settings.GetColor(PlanDesignationType);

            return ColorUtilities.GetColorDefByName(color);
        }
    }
}
