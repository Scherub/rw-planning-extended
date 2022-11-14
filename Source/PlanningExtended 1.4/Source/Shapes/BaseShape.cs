using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Variants;
using Verse;

namespace PlanningExtended.Shapes
{
    public abstract class BaseShape
    {
        int _selectedShapeIndex;

        public abstract List<BaseShapeVariant> ShapeVariants { get; }

        public BaseShapeVariant SelectedShapeVariant { get; private set; }

        public virtual ShapeOptions FirstShapeOption => SelectedShapeVariant.FirstShapeOption;

        public virtual ShapeOptions SecondShapeOption => SelectedShapeVariant.SecondShapeOption;

        public int NumberOfAvailableShapeOtions => SelectedShapeVariant.NumberOfAvailableShapeOtions;

        protected BaseShape(ShapeVariant defaultShapeVariant)
        {
            SelectShapeVariant(defaultShapeVariant);
        }

        public bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return SelectedShapeVariant.IsCellValid(cell, areaDimensions);
        }

        public void SelectShapeVariant(ShapeVariant shapeVariant)
        {
            SelectedShapeVariant = ShapeVariants.FirstOrDefault(sv => sv.ShapeVariant == shapeVariant);

            if (SelectedShapeVariant != null)
                _selectedShapeIndex = ShapeVariants.FindIndex(sv => sv.ShapeVariant == shapeVariant);
            else if (ShapeVariants.Count > 0)
                SelectedShapeVariant = ShapeVariants.First();
            else
                SelectedShapeVariant = new NullShapeVariant();
        }

        public void ChangeToNextShapeVariant()
        {
            if (_selectedShapeIndex >= ShapeVariants.Count - 1)
                _selectedShapeIndex = 0;
            else
                _selectedShapeIndex++;

            SelectedShapeVariant = ShapeVariants[_selectedShapeIndex];
        }
    }
}
