using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;
using Verse;

namespace PlanningExtended.Shapes.Variants
{
    internal abstract class BaseShapeGeneratorVariant<TShapeGenerator> : BaseShapeVariant
        where TShapeGenerator : BaseShapeGenerator
    {
        HashSet<IntVec3> _validCells = new();

        protected TShapeGenerator ShapeGenerator { get; }

        protected BaseShapeGeneratorVariant(BaseShapeModifier shapeModifier, TShapeGenerator shapeGenerator)
            : base(shapeModifier)
        {
            ShapeGenerator = shapeGenerator;
        }

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return _validCells.Contains(cell);
        }

        protected override void OnUpdateShape(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            _validCells = ShapeGenerator.Update(areaDimensions, mousePosition);
        }
    }
}
