using System.Collections.Generic;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using PlanningExtended.Plans.Appearances;
using PlanningExtended.Shapes.Variants.Fill;
using PlanningExtended.Shapes.Variants.FillColor;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators;

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
        return !IsColorPickModeEnabled && SelectedShape.SelectedShapeVariant is FillShapeVariant fillVariant
            ? DesignateFloodFill(cells, fillVariant)
            : base.DesignateMultiCellInternal(cells);
    }

    bool DesignateFloodFill(IEnumerable<IntVec3> cells, FillShapeVariant fillVariant)
    {
        // Find a valid starting cell for the flood fill.
        var startCell = cells.FirstOrFallback(c => CanDesignateCell(c).Accepted, IntVec3.Invalid);

        if (!startCell.IsValid)
            return false;

        HashSet<IntVec3> cellsToFill = fillVariant.ComputeFillCells(Map, startCell);

        if (cellsToFill.Count == 0)
            return false;

        var undoPlanLayout = CreateUndoPlanLayout(cellsToFill);

        foreach (IntVec3 cell in cellsToFill)
            PlanDesignationPlacerUtilities.Designate(Map, cell, SelectedDesignation, colorDef);

        CreateRedoPlanLayout(undoPlanLayout);
        return true;
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
    protected override IEnumerable<Shape> GetAdditionalShapes()
    {
        yield return Shape.Fill;
    }

    void TextureSetChanged(PlanDesignationType planDesignationType, PlanTextureSet planTextureSet)
    {
        if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == PlanDesignationType)
            icon = GetIcon(_name);
    }
}
