using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Gui.Designators.Shapes.ExtraControls;
using PlanningExtended.Shapes;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseShapePlanDesignator : BaseUndoRedoPlanDesignator
    {
        readonly ShapesManager _shapesManager = new();

        readonly ShapeExtraControlManager _shapeExtraControlManager = new();

        protected override bool HasLeftClickPopupMenu => true;

        protected BaseShape SelectedShape { get; private set; }

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

                CellArea cellArea = new(DesignationDragger.DragCells);

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
            List<FloatMenuOption> list = new();

            foreach (Shape shape in ShapesManager.AvailableShapes)
            {
                list.Add(new FloatMenuOption($"PlanningExtended.Shapes.{shape}".Translate(), () => {
                    Find.DesignatorManager.Select(this);
                    SelectShape(shape);
                }));
            }

            return list;
        }
    }
}
