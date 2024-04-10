using RimWorld;
using Verse;

namespace PlanningExtended.Shapes.Features
{
    public class SegmentShapeFeature : BaseShapeFeature
    {
        readonly bool _useNumberOfCells;

        public override KeyBindingDef KeyBindingLeft => KeyBindingDefOf.Designator_RotateLeft;

        public override KeyBindingDef KeyBindingRight => KeyBindingDefOf.Designator_RotateRight;

        public KeyBindingDef KeyBindingLeft2 => PlanningKeyBindingDefOf.Planning_Action1;

        public KeyBindingDef KeyBindingRight2 => PlanningKeyBindingDefOf.Planning_Action2;

        public int NumberOfColumns => _useNumberOfCells ? NumberOfSegmentsX + 1 : NumberOfSegmentsX;

        public int NumberOfRows => _useNumberOfCells ? NumberOfSegmentsZ + 1 : NumberOfSegmentsZ;

        public override ShapeDisplayOptions DisplayOptions => ShapeDisplayOptions.NumberOfSegments;

        public override ShapeOptions ShapeOptions => ShapeOptions.NumberOfSegmentsX;

        public ShapeOptions ShapeOptions2 => ShapeOptions.NumberOfSegmentsZ;

        int _numberOfSegmentsX = 1;
        public int NumberOfSegmentsX
        {
            get => _numberOfSegmentsX;
            private set
            {
                if (_numberOfSegmentsX == value)
                    return;

                _numberOfSegmentsX = value;
                RequestUpdate();
            }
        }

        int _numberOfSegmentsZ = 1;
        public int NumberOfSegmentsZ
        {
            get => _numberOfSegmentsZ;
            private set
            {
                if (_numberOfSegmentsZ == value)
                    return;

                _numberOfSegmentsZ = value;
                RequestUpdate();
            }
        }

        public SegmentShapeFeature(bool useNumberOfCells)
        {
            _useNumberOfCells = useNumberOfCells;
        }

        public override void HandleKeyboardInput()
        {
            base.HandleKeyboardInput();

            if (KeyBindingLeft2?.KeyDownEvent == true)
                ChangeShapeOption(ShapeOptions2, ShapeOptionDirection.Left);
            else if (KeyBindingRight2?.KeyDownEvent == true)
                ChangeShapeOption(ShapeOptions2, ShapeOptionDirection.Right);
        }

        public override void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptions == ShapeOptions)
                NumberOfSegmentsX = GetAdjustedNumberOfSegments(shapeOptionDirection, NumberOfSegmentsX);
            else if (shapeOptions == ShapeOptions2)
                NumberOfSegmentsZ = GetAdjustedNumberOfSegments(shapeOptionDirection, NumberOfSegmentsZ);
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
