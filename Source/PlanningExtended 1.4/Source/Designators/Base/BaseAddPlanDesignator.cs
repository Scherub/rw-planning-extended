using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
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

            return !PlanningKeyBindingDefOf.Planning_Modifier.IsDown || !Map.designationManager.HasPlanDesignationAt(c);
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
            PlanningMod.Settings.Write();
        }

        protected override ColorDef GetColorDef()
        {
            string color = PlanningMod.Settings.GetColor(PlanDesignationType);

            return ColorUtilities.GetColorDefByName(color);
        }
    }
}
