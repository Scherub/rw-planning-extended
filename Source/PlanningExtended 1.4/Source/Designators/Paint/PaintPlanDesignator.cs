using System;
using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class PaintPlanDesignator : BaseColorPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanDoors;

        protected override DesignationDef ColoredDesignation => throw new NotImplementedException();

        protected override Action<Rect> OnPostDrawMouseAttachment => (rect) => GUI.DrawTexture(rect, IconTopTexture);

        Texture2D IconTopTexture => ContentFinder<Texture2D>.Get("UI/Designators/PaintPlan_Top", true);

        public PaintPlanDesignator()
            : base("PaintPlan")
        {
            soundSucceeded = SoundDefOf.Designate_Paint;
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

            return Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (IsColorPickModeEnabled)
            {
                colorPicker.DesignateSingleCell(c);
                return;
            }

            PlanDesignation planDesignation = Map.designationManager.GetOnlyPlanDesignationAt(c);

            planDesignation.colorDef = colorDef;

            planDesignation.InvokeUpdate(PlanDesignationType.Unknown, PlanDesignationUpdateType.Color);
        }

        //public override void RenderHighlight(List<IntVec3> dragCells)
        //{
        //    CellArea cellArea = new(dragCells);
        //    AreaDimensions areaDimensions = cellArea.Dimensions;

        //    List<IntVec3> cells = new();

        //    foreach (IntVec3 cell in dragCells)
        //        if (IsShapeCellValid(cell, areaDimensions))
        //            cells.Add(cell);

        //    DesignatorUtility.RenderHighlightOverSelectableCells(this, cells);
        //}

        public override void DoExtraGuiControls(float leftX, float bottomY)
        {
            DrawExtraGuiControls(leftX, bottomY);
        }

        public override void DrawIcon(Rect rect, Material buttonMat, GizmoRenderParms parms)
        {
            base.DrawIcon(rect, buttonMat, parms);

            Widgets.DrawTextureFitted(rect, IconTopTexture, iconDrawScale * 0.85f, iconProportions, iconTexCoords, iconAngle, buttonMat);
        }

        protected override void SetColorDef(ColorDef newColorDef)
        {
            base.SetColorDef(newColorDef);

            Settings.paintPlanColor = newColorDef.defName;
            Settings.Write();
        }

        protected override ColorDef GetColorDef()
        {
            string color = PlanningMod.Settings.paintPlanColor;

            return ColorUtilities.GetColorDefByName(color);
        }
    }
}
