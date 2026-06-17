using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Gui.Designators.Shapes.ExtraControls;
using PlanningExtended.Shapes;
using PlanningExtended.Shapes.Variants.Fill;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators;

public abstract class BaseShapePlanDesignator : BaseUndoRedoPlanDesignator
{
    readonly ShapesManager _shapesManager = new();

    readonly ShapeExtraControlManager _shapeExtraControlManager = new();

    protected override bool HasLeftClickPopupMenu => true;

    protected BaseShape SelectedShape { get; private set; }

    protected bool IsFloodFillShapeVariant => SelectedShape?.SelectedShapeVariant is BaseFloodFillShapeVariant;

    protected override bool MakeAutoSnapshots => !IsFloodFillShapeVariant;

    public override DrawStyleCategoryDef DrawStyleCategory => DrawStyleCategoryDefOf.FilledRectangle;

    public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetShapesMenuOptions();

    protected BaseShapePlanDesignator(string name)
        : base(name)
    {
        SelectShape(Shape.Rectangle);
    }

    protected void SelectShape(Shape shape)
    {
        SelectedShape = _shapesManager.GetShape(shape);
    }

    public override void SelectedProcessInput(Event ev)
    {
        HandleShortcuts();
    }

    //protected override bool ShowLeftClickPopupMenu()
    //{
    //    if (base.ShowLeftClickPopupMenu())
    //        return true;

    //    if (!KeyBindingDefOf.ShowEyedropper.IsDown)
    //    {
    //        List<FloatMenuOption> list = GetShapesMenuOptions();

    //        Find.WindowStack.Add(new FloatMenu(list));

    //        return true;
    //    }

    //    return false;
    //}

    public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
    {
        if (IsCenterModeKeyPressed)
            cells = CellUtilities.GetCenterModeCells(cells, new IntVec3(UI.MouseMapPosition()));

        base.DesignateMultiCell(cells);
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

    public override void RenderHighlight(List<IntVec3> dragCells)
    {
        List<IntVec3> sourceCells;

        if (IsCenterModeKeyPressed)
        {
            sourceCells = [];

            foreach (IntVec3 cell in CellUtilities.GetCenterModeCells(dragCells, new IntVec3(UI.MouseMapPosition())))
                if (cell.InBounds(Map) && !cell.InNoBuildEdgeArea(Map))
                    sourceCells.Add(cell);
        }
        else
        {
            sourceCells = dragCells;
        }

        CellArea cellArea = new(sourceCells);
        AreaDimensions areaDimensions = cellArea.Dimensions;

        List<IntVec3> cells = [];

        foreach (IntVec3 cell in sourceCells)
            if (IsShapeCellValid(cell, areaDimensions))
                cells.Add(cell);

        DesignatorUtility.RenderHighlightOverSelectableCells(this, cells);
    }

    protected virtual IEnumerable<Shape> GetAdditionalShapes() => [];

    protected virtual bool IsShapeCellValid(IntVec3 cell, AreaDimensions areaDimensions)
    {
        return SelectedShape.IsCellValid(cell, areaDimensions);
    }

    protected void DrawExtraGuiControls(float leftX, float bottomY)
    {
        _shapeExtraControlManager.DrawExtraControls(leftX, bottomY, SelectedShape);
    }

    protected override void OnDrawMouseAttachment()
    {
        base.OnDrawMouseAttachment();

        if (DesignationDragger.Dragging)
        {
            IntVec3 mousePosition = new(UI.MouseMapPosition());

            CellArea cellArea = IsCenterModeKeyPressed
                ? new(CellUtilities.GetCenterModeCells(DesignationDragger.DragCells, mousePosition))
                : new(DesignationDragger.DragCells);

            SelectedShape?.UpdateShape(cellArea.Dimensions, mousePosition, IsModifierKeyPressed);
        }
    }

    void HandleShortcuts()
    {
        if (PlanningKeyBindingDefOf.Planning_ChangeShapeVariant.KeyDownEvent)
            SelectedShape.ChangeToNextShapeVariant();

        SelectedShape.SelectedShapeVariant.ShapeFeatureManager.HandleKeyboardInput();
    }

    List<FloatMenuOption> GetShapesMenuOptions()
    {
        List<FloatMenuOption> list =
        [
            ..ShapesManager.AvailableShapes
                .Concat(GetAdditionalShapes())
                .Select(shape => new FloatMenuOption(shape.Translate(), () =>
                {
                    Find.DesignatorManager.Select(this);
                    SelectShape(shape);
                }))
        ];

        return list;
    }
}
