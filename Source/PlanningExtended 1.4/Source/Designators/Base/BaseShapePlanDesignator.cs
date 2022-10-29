using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseShapePlanDesignator : BaseUndoRedoPlanDesignator
    {
        readonly ShapesManager _shapesManager = new();

        protected override bool HasPopupMenu => true;

        protected BaseShape SelectedShape { get; private set; }

        protected BaseShapePlanDesignator(string name)
            : base(name)
        {
            SelectShape(Shape.Rectangle);
        }

        protected void SelectShape(Shape shape)
        {
            SelectedShape = _shapesManager.GetShape(shape);
        }

        protected override void ShowPopupMenu()
        {
            base.ShowPopupMenu();

            if (!KeyBindingDefOf.ShowEyedropper.IsDown)
            {
                List<FloatMenuOption> list = new();

                foreach (Shape shape in ShapesManager.AvailableShapes)
                {
                    list.Add(new FloatMenuOption(shape.ToString(), () => {
                        Find.DesignatorManager.Select(this);
                        SelectShape(shape);
                    }));
                }

                Find.WindowStack.Add(new FloatMenu(list));
            }
        }

        protected virtual bool IsShapeCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return SelectedShape.IsCellValid(cell, areaDimensions);
        }
    }
}
