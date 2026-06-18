using System;
using System.Collections.Generic;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using PlanningExtended.Shapes.Variants.FillColor;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators;

public class PaintPlanDesignator : BaseColorPlanDesignator
{
    protected override DesignationDef Designation => PlanningDesignationDefOf.PlanDoors;

    protected override DesignationDef ColoredDesignation => throw new NotImplementedException();

    protected override Action<Rect> OnPostDrawMouseAttachment => (rect) => GUI.DrawTexture(rect, IconTopTexture);

    Texture2D IconTopTexture => ContentFinder<Texture2D>.Get("UI/Designators/PaintPlan_Top", true);

    public bool IsFillShapeActive => SelectedShape.SelectedShapeVariant is BaseFillColorShapeVariant;

    public PaintPlanDesignator()
        : base("PaintPlan")
    {
        soundSucceeded = SoundDefOf.Designate_Paint;
    }

    protected override bool DesignateMultiCellInternal(IEnumerable<IntVec3> cells)
    {
        return !IsColorPickModeEnabled && SelectedShape.SelectedShapeVariant is BaseFillColorShapeVariant fillVariant
            ? DesignateFloodFill(cells, fillVariant)
            : base.DesignateMultiCellInternal(cells);
    }

    bool DesignateFloodFill(IEnumerable<IntVec3> cells, BaseFillColorShapeVariant fillVariant)
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
        {
            PlanDesignation designation = Map.designationManager.GetOnlyPlanDesignationAt(cell);
            if (designation != null)
            {
                designation.colorDef = colorDef;
                designation.InvokeUpdate(PlanDesignationType.Unknown, PlanDesignationUpdateType.Color);
            }
        }

        CreateRedoPlanLayout(undoPlanLayout);
        return true;
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

    protected override IEnumerable<Shape> GetAdditionalShapes()
    {
        yield return Shape.FillColor;
    }
}
