﻿using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Variants;
using Verse;

namespace PlanningExtended.Shapes
{
    public abstract class BaseShape
    {
        int _selectedShapeIndex;

        readonly List<BaseShapeVariant> _shapeVariants;

        public BaseShapeVariant SelectedShapeVariant { get; private set; }

        public virtual ShapeOptions FirstShapeOption => SelectedShapeVariant.FirstShapeOption;

        public virtual ShapeOptions SecondShapeOption => SelectedShapeVariant.SecondShapeOption;

        public int NumberOfAvailableShapeOtions => SelectedShapeVariant.NumberOfAvailableShapeOtions;

        public IEnumerable<ShapeOptions> ShapeOptions
        {
            get
            {
                yield return FirstShapeOption;
                yield return SecondShapeOption;
            }
        }

        public ShapeDisplayOptions ShapeDisplayOptions => SelectedShapeVariant.ShapeDisplayOptions;

        protected BaseShape(ShapeVariant defaultShapeVariant, params BaseShapeVariant[] shapeVariants)
        {
            _shapeVariants = new(shapeVariants);

            SelectShapeVariant(defaultShapeVariant);
        }

        public void UpdateShape(AreaDimensions areaDimensions, IntVec3 intVec3, bool applyModifier)
        {
            SelectedShapeVariant.UpdateShape(areaDimensions, intVec3, applyModifier);
        }

        public bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return SelectedShapeVariant.IsCellValid(cell, areaDimensions);
        }

        public void SelectShapeVariant(ShapeVariant shapeVariant)
        {
            SelectedShapeVariant = _shapeVariants.FirstOrDefault(sv => sv.ShapeVariant == shapeVariant);

            if (SelectedShapeVariant != null)
                _selectedShapeIndex = _shapeVariants.FindIndex(sv => sv.ShapeVariant == shapeVariant);
            else if (_shapeVariants.Count > 0)
                SelectedShapeVariant = _shapeVariants.First();
            else
                SelectedShapeVariant = new NullShapeVariant();
        }

        public void ChangeToNextShapeVariant()
        {
            if (_selectedShapeIndex >= _shapeVariants.Count - 1)
                _selectedShapeIndex = 0;
            else
                _selectedShapeIndex++;

            SelectedShapeVariant = _shapeVariants[_selectedShapeIndex];
        }
    }
}
