using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers;

namespace PlanningExtended.Shapes.Variants
{
    internal abstract class BaseShapeSegmentsVariant<TShapeGenerator> : BaseShapeGeneratorVariant<TShapeGenerator>, IShapeSegmentVariant
            where TShapeGenerator : BaseShapeSegmentGenerator
    {
        readonly bool _useNumberOfCells;

        int _numberOfSegmentsX = 1;

        int _numberOfSegmentsZ = 1;

        public override ShapeDisplayOptions ShapeDisplayOptions => base.ShapeDisplayOptions | ShapeDisplayOptions.NumberOfSegments;

        public int NumberOfColumns => _useNumberOfCells ? _numberOfSegmentsX + 1 : _numberOfSegmentsX;

        public int NumberOfRows => _useNumberOfCells ? _numberOfSegmentsZ + 1 : _numberOfSegmentsZ;

        protected BaseShapeSegmentsVariant(BaseShapeModifier shapeModifier, TShapeGenerator shapeGenerator, bool useNumberOfCells)
            : base(shapeModifier, shapeGenerator)
        {
            _useNumberOfCells = useNumberOfCells;
        }

        public override void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptions is ShapeOptions.NumberOfSegmentsX)
                _numberOfSegmentsX = GetAdjustedNumberOfSegments(shapeOptionDirection, _numberOfSegmentsX);
            else if (shapeOptions is ShapeOptions.NumberOfSegmentsZ)
                _numberOfSegmentsZ = GetAdjustedNumberOfSegments(shapeOptionDirection, _numberOfSegmentsZ);

            if (shapeOptions is ShapeOptions.NumberOfSegmentsX or ShapeOptions.NumberOfSegmentsZ)
                ShapeGenerator.UpdateNumberOfSegments(_numberOfSegmentsX, _numberOfSegmentsZ);
        }

        int GetAdjustedNumberOfSegments(ShapeOptionDirection shapeOptionDirection, int currentNummberOfSegments)
        {
            if (shapeOptionDirection is ShapeOptionDirection.Left && currentNummberOfSegments > 1)
                return currentNummberOfSegments - 1;
            else if (shapeOptionDirection is ShapeOptionDirection.Right && currentNummberOfSegments < 5)
                return currentNummberOfSegments + 1;

            return currentNummberOfSegments;
        }
    }
}
