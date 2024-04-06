using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Designations;
using PlanningExtended.Plans.Appearances;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseAddPlanDesignator : BaseColorPlanDesignator
    {
        readonly string _name;

        protected override bool HasLeftClickPopupMenu => true;

        public abstract PlanDesignationType PlanDesignationType { get; }

        public override Color IconDrawColor => colorDef != null ? colorDef.color : Color.white;

        protected BaseAddPlanDesignator(string name)
            : base(name)
        {
            _name = name;

            PlanAppearanceManager.PlanColorChanged += PlanAppearanceManager_ColorChanged;
            PlanAppearanceManager.TextureSetChanged += TextureSetChanged;
        }

        protected override bool DesignateMultiCellInternal(IEnumerable<IntVec3> cells)
        {
            CellArea cellArea = new(cells);
            AreaDimensions areaDimensions = cellArea.Dimensions;

            bool somethingSucceeded = false;
            bool flag = false;

            foreach (IntVec3 cell in cells)
            {
                if (CanDesignateCell(cell).Accepted && IsShapeCellValid(cell, areaDimensions))
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

            return OverwriteDesignation || !Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (IsColorPickModeEnabled)
            {
                colorPicker.DesignateSingleCell(c);
                return;
            }

            PlanDesignationPlacerUtilities.Designate(Map, c, SelectedDesignation, colorDef);
        }

        //public override void DrawPanelReadout(ref float curY, float width)
        //{
        //    //base.DrawPanelReadout(ref curY, width);
        //    Widgets.InfoCardButton(width - 24f - 2f, 6f, this.entDef);

        //}

        public override void DoExtraGuiControls(float leftX, float bottomY)
        {
            DrawExtraGuiControls(leftX, bottomY);
        }

        protected override void OnSkipExistingDesignationsKeyChanged(bool isPressed)
        {
            ResetMouseAttachmentText();
        }

        protected override Texture2D GetIcon(string name)
        {
            return ContentFinder<Texture2D>.Get($"UI/Designators/{PlanAppearanceManager.GetPlanTextureSet(PlanDesignationType)}/{name}", true);
        }

        protected override string GetMouseAttachmentText()
        {
            return $"{"PlanningExtended.Mode".Translate()}: {GetSkipReplaceModeString()}\n" + base.GetMouseAttachmentText();
        }

        protected override ColorDef GetColorDef()
        {
            return PlanAppearanceManager.GetPlanColor(PlanDesignationType);
        }

        protected override void OnColorSelection(ColorDef colorDef)
        {
            PlanAppearanceManager.SetPlanColor(PlanDesignationType, colorDef);
        }

        void PlanAppearanceManager_ColorChanged(PlanDesignationType planDesignationType, ColorDef newColorDef)
        {
            Log.Warning($"PlanChangeColorButton '{PlanDesignationType}': Color changed to '{newColorDef.defName}' for '{planDesignationType}'");

            if (planDesignationType != PlanDesignationType.Unknown && planDesignationType != PlanDesignationType)
                return;

            colorDef = newColorDef;

            ResetMouseAttachmentText();
        }

        void TextureSetChanged(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == PlanDesignationType)
                icon = GetIcon(_name);
        }
    }
}
