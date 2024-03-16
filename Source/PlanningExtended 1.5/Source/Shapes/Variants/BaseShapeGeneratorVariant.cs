using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    internal abstract class BaseShapeGeneratorVariant<TShapeGenerator> : BaseShapeVariant
        where TShapeGenerator : BaseShapeGenerator
    {
        HashSet<IntVec3> _validCells = new();

        protected TShapeGenerator ShapeGenerator { get; }

        protected BaseShapeGeneratorVariant(BaseShapeDimensionsModifier shapeModifier, TShapeGenerator shapeGenerator, params IShapeFeature[] shapeFeatures)
            : base(shapeModifier, shapeFeatures)
        {
            ShapeGenerator = shapeGenerator;
        }

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return _validCells.Contains(cell);
        }

        protected override void OnUpdateShape(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            _validCells = ShapeGenerator.Update(shape, areaDimensions, mousePosition, rotation, applyShapeDimensionsModifier);
        }
    }
}
