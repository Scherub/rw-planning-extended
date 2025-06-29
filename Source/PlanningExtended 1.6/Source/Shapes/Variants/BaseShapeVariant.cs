using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Modifiers.Dimensions;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    public abstract class BaseShapeVariant
    {
        readonly BaseShapeDimensionsModifier _shapeModifier;

        protected Direction Rotation => ShapeFeatureManager.RotationShapeFeature?.Rotation ?? Direction.None;

        public abstract ShapeVariant ShapeVariant { get; }

        public ShapeDisplayOptions ShapeDisplayOptions => ShapeFeatureManager.ShapeDisplayOptions;

        public ShapeFeatureManager ShapeFeatureManager { get; }

        protected BaseShapeVariant(BaseShapeDimensionsModifier shapeModifier, params IShapeFeature[] shapeFeatures)
        {
            _shapeModifier = shapeModifier;

            ShapeFeatureManager = new ShapeFeatureManager(shapeFeatures);
        }

        public void UpdateShape(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            if (applyShapeDimensionsModifier)
                areaDimensions = _shapeModifier.Update(shape, areaDimensions, mousePosition, Rotation);

            OnUpdateShape(shape, areaDimensions, mousePosition, Rotation, applyShapeDimensionsModifier);
        }

        public abstract bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions);

        protected virtual void OnUpdateShape(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {

        }
    }
}
