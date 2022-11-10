using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Gui;
using PlanningExtended.Shapes;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseShapePlanDesignator : BaseUndoRedoPlanDesignator
    {
        readonly ShapesManager _shapesManager = new();

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

        protected virtual bool IsShapeCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return SelectedShape.IsCellValid(cell, areaDimensions);
        }

        //public override void DoExtraGuiControls(float leftX, float bottomY)
        //{
        //    float width = 200f;
        //    float height = 180f;

        //    Rect winRect = new(leftX, bottomY - height, width, height);

        //    Find.WindowStack.ImmediateWindow(73095, winRect, WindowLayer.GameUI, () =>
        //    {
        //        Text.Anchor = TextAnchor.MiddleCenter;
        //        Text.Font = GameFont.Medium;

        //        RotationDirection rotationDirection = RotationDirection.None;
        //        FlipDirection flipDirection = FlipDirection.None;

        //        ShapeOptionDirection shapeOptionDirection1 = ShapeOptionDirection.None;
        //        ShapeOptionDirection shapeOptionDirection2 = ShapeOptionDirection.None;

        //        Rect rect = new(winRect.width / 2f - 64f - 5f, 15f, 64f, 64f);

        //        rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, Textures.RotateLeft, KeyBindingDefOf.Designator_RotateLeft.MainKeyLabel, RotationDirection.Counterclockwise, rotationDirection);

        //        rect = new(winRect.width / 2f + 5f, 15f, 64f, 64f);

        //        rotationDirection = GuiUtilities.DrawButtonImageRotation(rect, Textures.RotateRight, KeyBindingDefOf.Designator_RotateRight.MainKeyLabel, RotationDirection.Clockwise, rotationDirection);

        //        rect = new(winRect.width / 2f - 64f - 5f, winRect.height / 2f + 15f, 64f, 64f);

        //        flipDirection = GuiUtilities.DrawButtonImageFlip(rect, Textures.FlipHorizontal, PlanningKeyBindingDefOf.Planning_Action1.MainKeyLabel, FlipDirection.Horizontally, flipDirection);

        //        rect = new(winRect.width / 2f + 5f, winRect.height / 2f + 15f, 64f, 64f);

        //        flipDirection = GuiUtilities.DrawButtonImageFlip(rect, Textures.FlipVertical, PlanningKeyBindingDefOf.Planning_Action2.MainKeyLabel, FlipDirection.Vertically, flipDirection);

        //        Text.Anchor = TextAnchor.UpperLeft;
        //        Text.Font = GameFont.Small;
        //    }, true, false, 1f, null);
        //}

        void HandleShortcuts()
        {
            ShapeOptionDirection shapeOptionDirection1 = ShapeOptionDirection.None;
            ShapeOptionDirection shapeOptionDirection2 = ShapeOptionDirection.None;

            if (PlanningKeyBindingDefOf.Planning_ChangeShapeVariant.KeyDownEvent)
                SelectedShape.ChangeToNextShapeVariant();

            if (KeyBindingDefOf.Designator_RotateLeft.KeyDownEvent)
                shapeOptionDirection1 = ShapeOptionDirection.Left;
            if (KeyBindingDefOf.Designator_RotateRight.KeyDownEvent)
                shapeOptionDirection1 = ShapeOptionDirection.Right;

            if (PlanningKeyBindingDefOf.Planning_Action1.KeyDownEvent)
                shapeOptionDirection2 = ShapeOptionDirection.Left;
            if (PlanningKeyBindingDefOf.Planning_Action2.KeyDownEvent)
                shapeOptionDirection2 = ShapeOptionDirection.Right;

            SelectedShape.SelectedShapeVariant.ChangeFirstShapeOption(shapeOptionDirection1);
            SelectedShape.SelectedShapeVariant.ChangeSecondShapeOption(shapeOptionDirection2);
        }

        List<FloatMenuOption> GetShapesMenuOptions()
        {
            List<FloatMenuOption> list = new();

            foreach (Shape shape in ShapesManager.AvailableShapes)
            {
                list.Add(new FloatMenuOption(shape.ToString(), () => {
                    Find.DesignatorManager.Select(this);
                    SelectShape(shape);
                }));
            }

            return list;
        }
    }
}
