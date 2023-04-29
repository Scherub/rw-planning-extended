using PlanningExtended.Cells;
using PlanningExtended.Shapes.Modifiers;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    public abstract class BaseShapeVariant
    {
        readonly BaseShapeModifier _shapeModifier;

        public abstract ShapeVariant ShapeVariant { get; }

        public virtual ShapeOptions FirstShapeOption { get; }

        public virtual ShapeOptions SecondShapeOption { get; }

        public virtual ShapeDisplayOptions ShapeDisplayOptions => ShapeDisplayOptions.DisplayVariant;

        public int NumberOfAvailableShapeOtions => NumShapeOptions(FirstShapeOption) + NumShapeOptions(SecondShapeOption);

        protected BaseShapeVariant(BaseShapeModifier shapeModifier)
        {
            _shapeModifier = shapeModifier;
        }

        public void UpdateShape(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            if (applyShapeDimensionsModifier)
                areaDimensions = _shapeModifier.Update(areaDimensions, mousePosition);

            OnUpdateShape(areaDimensions, mousePosition, applyShapeDimensionsModifier);
        }

        public abstract bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions);

        public virtual void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {

        }

        public void ChangeFirstShapeOption(ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptionDirection is not ShapeOptionDirection.None)
                ChangeShapeOption(FirstShapeOption, shapeOptionDirection);
        }

        public void ChangeSecondShapeOption(ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptionDirection is not ShapeOptionDirection.None)
                ChangeShapeOption(SecondShapeOption, shapeOptionDirection);
        }

        protected virtual void OnUpdateShape(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {

        }

        int NumShapeOptions(ShapeOptions shapeOptions)
        {
            return shapeOptions != ShapeOptions.None ? 1 : 0;
        }
    }
}
