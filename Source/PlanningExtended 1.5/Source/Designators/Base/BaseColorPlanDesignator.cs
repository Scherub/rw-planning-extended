using System;
using System.Collections.Generic;
using PlanningExtended.Gui.Utilities;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseColorPlanDesignator : BaseShapePlanDesignator
    {
        bool UseCtrlForColorDialog => PlanningMod.Settings.General.useCtrlForColorDialog;

        protected ColorPickerDesignator colorPicker;

        protected ColorDef colorDef;

        protected bool IsColorPickModeEnabled { get; private set; }

        protected abstract DesignationDef ColoredDesignation { get; }

        protected DesignationDef SelectedDesignation => colorDef == ColorDefinitions.NonColoredDef ? Designation : ColoredDesignation;

        protected virtual Action<Rect> OnPostDrawMouseAttachment => null;

        public override int DraggableDimensions => IsColorPickModeEnabled ? colorPicker.DraggableDimensions : base.DraggableDimensions;

        public override Color IconDrawColor => colorDef.color;

        public ColorPickerDesignator ColorPicker => colorPicker;

        protected BaseColorPlanDesignator(string name)
            : base(name)
        {
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate(PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel, PlanningKeyBindingDefOf.Planning_Modifier.MainKeyLabel);

            colorDef = GetColorDef();

            colorPicker = new ColorPickerDesignator((newColorDef) =>
            {
                OnColorSelection(newColorDef);

                if (!IsColorPickModeEnabled)
                    Find.DesignatorManager.Select(this);
            }, "PlanningExtended.SelectColoredPlan".Translate(), "PlanningExtended.PickColorFromPlan".Translate());
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
                GenUI.DrawMouseAttachment(icon, MouseAttachmentText, iconAngle, iconOffset, null, false, default, new Color?(colorDef.color), OnPostDrawMouseAttachment);
        }

        protected override bool ShowLeftClickPopupMenu()
        {
            if (base.ShowLeftClickPopupMenu())
                return true;

            if (!UseCtrlForColorDialog || PlanningKeyBindingDefOf.Planning_ColorPicker.IsDown)
            {
                List<FloatMenuGridOption> list = GuiMenuOptionsUtilities.GetColorSelectionMenuGridOptions(colorPicker, (colorDef) =>
                {
                    OnColorSelection(colorDef);
                    Find.DesignatorManager.Select(this);
                });

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

        protected abstract ColorDef GetColorDef();

        protected abstract void OnColorSelection(ColorDef colorDef);
    }
}
