using System;
using System.Collections.Generic;
using PlanningExtended.Cells;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators;

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
        return IsColorPickModeEnabled || base.IsShapeCellValid(cell, areaDimensions);
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

    protected override void OnModifierKeyChanged(bool isPressed)
    {
        ResetMouseAttachmentText();
    }

    protected override void OnCenterModeKeyChanged(bool isPressed)
    {
        ResetMouseAttachmentText();
    }

    protected override string GetMouseAttachmentText()
    {
        return "Color".Translate() + ": " + colorDef.LabelCap + "\n"
            + GetPressModifierKeyString()
            + GetPressedModfierKeyString();
    }

    protected virtual void SetColorDef(ColorDef newColorDef)
    {
        colorDef = newColorDef ?? ColorDefinitions.NonColoredDef;
    }

    protected virtual ColorDef GetColorDef()
    {
        return ColorDefinitions.NonColoredDef;
    }

    string GetPressModifierKeyString()
    {
        if (IsColorPickModeEnabled || IsModifierKeyPressed || IsCenterModeKeyPressed || IsSkipExistingDesignationsKeyPressed)
            return "";

        List<string> modifierKeys = [];

        if (PlanningKeyBindingDefOf.Planning_ColorPicker.MainKey != KeyCode.None)
            modifierKeys.Add(PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel);

        if (PlanningKeyBindingDefOf.Planning_Modifier.MainKey != KeyCode.None)
            modifierKeys.Add(PlanningKeyBindingDefOf.Planning_Modifier.MainKeyLabel);

        if (PlanningKeyBindingDefOf.Planning_CenterDrawingMode.MainKey != KeyCode.None)
            modifierKeys.Add(PlanningKeyBindingDefOf.Planning_CenterDrawingMode.MainKeyLabel);

        if (PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.MainKey != KeyCode.None)
            modifierKeys.Add(PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.MainKeyLabel);

        return "Press " + string.Join(", ", modifierKeys);
    }

    string GetPressedModfierKeyString()
    {
        string result = "";

        if (IsModifierKeyPressed)
            result += $"{PlanningKeyBindingDefOf.Planning_Modifier.MainKeyLabel}: {"PlanningExtended.ShapeModifier".Translate()}\n";

        if (IsCenterModeKeyPressed)
            result += $"{PlanningKeyBindingDefOf.Planning_CenterDrawingMode.MainKeyLabel}: {"PlanningExtended.CenterDrawingMode".Translate()}\n";

        if (IsSkipExistingDesignationsKeyPressed)
            result += $"{PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.MainKeyLabel}: " +
            (Settings.useSkipInsteadOfReplaceAsDefault
            ? (IsSkipExistingDesignationsKeyPressed ? "PlanningExtended.PlaceMode.Replace".Translate() : "PlanningExtended.PlaceMode.Skip".Translate())
            : (IsSkipExistingDesignationsKeyPressed ? "PlanningExtended.PlaceMode.Skip".Translate() : "PlanningExtended.PlaceMode.Replace".Translate()));

        return result;
    }
}
