using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Colors;
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

        protected abstract PlanDesignationType PlanDesignationType { get; }

        protected override bool HasLeftClickPopupMenu => true;

        public override Color IconDrawColor => colorDef != null ? colorDef.color : Color.white;

        protected BaseAddPlanDesignator(string name)
            : base(name)
        {
            _name = name;

            PlanAppearanceManager.TextureSetChanged -= TextureSetChanged;
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

        void TextureSetChanged(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == PlanDesignationType)
                icon = GetIcon(_name);
        }
    }
}
