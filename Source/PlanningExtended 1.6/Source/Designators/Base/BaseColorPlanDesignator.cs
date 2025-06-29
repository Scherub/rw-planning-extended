using System;
using System.Collections.Generic;
using PlanningExtended.Cells;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseColorPlanDesignator : BaseShapePlanDesignator
    {
        //public override DrawStyleCategoryDef DrawStyleCategory => IsColorPickModeEnabled ? colorPicker.DrawStyleCategory : base.DrawStyleCategory;

        public override Color IconDrawColor => colorDef.color;

        protected ColorPickerDesignator colorPicker;

        protected ColorDef colorDef;

        protected bool IsColorPickModeEnabled { get; private set; }

        protected abstract DesignationDef ColoredDesignation { get; }

        protected DesignationDef SelectedDesignation => colorDef == ColorDefinitions.NonColoredDef ? Designation : ColoredDesignation;

        protected virtual Action<Rect> OnPostDrawMouseAttachment => null;

        bool UseCtrlForColorDialog => PlanningMod.Settings.useCtrlForColorDialog;

        protected BaseColorPlanDesignator(string name)
            : base(name)
        {
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate(PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel, PlanningKeyBindingDefOf.Planning_Modifier.MainKeyLabel);

            colorDef = GetColorDef();

            colorPicker = new ColorPickerDesignator((newColorDef) =>
            {
                SetColorDef(newColorDef);
                ResetMouseAttachmentText();

                if (!IsColorPickModeEnabled)
                    Find.DesignatorManager.Select(this);
            }, "PlanningExtended.SelectColoredPlan".Translate(), "PlanningExtended.PickColorFromPlan".Translate());
        }

        protected override bool IsShapeCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return IsColorPickModeEnabled ? true : base.IsShapeCellValid(cell, areaDimensions);
        }

        public override void DrawMouseAttachments()
        {
            CheckPressedKeys();

            if (IsColorPickModeEnabled)
            {
                colorPicker.DrawMouseAttachments();
                return;
            }

            OnDrawMouseAttachment();

            if (useMouseIcon)
                GenUI.DrawMouseAttachment(icon, MouseAttachmentText, iconAngle, iconOffset, null, null, false, default, new Color?(colorDef.color), OnPostDrawMouseAttachment);
        }

        protected override bool ShowLeftClickPopupMenu()
        {
            if (base.ShowLeftClickPopupMenu())
                return true;

            if (!UseCtrlForColorDialog || PlanningKeyBindingDefOf.Planning_ColorPicker.IsDown)
            {
                List<FloatMenuGridOption> list = new(ColorDefinitions.ColorDefs.Count + 1)
                {
                    new FloatMenuGridOption(Designator_Eyedropper.EyeDropperTex, () =>
                    {
                        Find.DesignatorManager.Select(colorPicker);
                    }, null, new TipSignal?("DesignatorEyeDropperDesc_Paint".Translate()))
                };

                foreach (ColorDef colorDef in ColorDefinitions.ColorDefs)
                {
                    list.Add(new FloatMenuGridOption(BaseContent.WhiteTex, () =>
                    {
                        Find.DesignatorManager.Select(this);
                        SetColorDef(colorDef);
                        ResetMouseAttachmentText();

                    }, new Color?(colorDef.color), new TipSignal?(colorDef.LabelCap)));
                }

                Find.WindowStack.Add(new FloatMenuGrid(list));

                return true;
            }

            return false;
        }

        protected override void CheckPressedKeys()
        {
            base.CheckPressedKeys();

            IsColorPickModeEnabled = PlanningKeyBindingDefOf.Planning_ColorPicker.IsDown;
        }

        protected override string GetMouseAttachmentText()
        {
            return "Color".Translate() + ": " + colorDef.LabelCap + "\n" + PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel + ": " + "GrabExistingColor".Translate();
        }

        protected virtual void SetColorDef(ColorDef newColorDef)
        {
            colorDef = newColorDef ?? ColorDefinitions.NonColoredDef;
        }

        protected virtual ColorDef GetColorDef()
        {
            return ColorDefinitions.NonColoredDef;
        }
    }
}
