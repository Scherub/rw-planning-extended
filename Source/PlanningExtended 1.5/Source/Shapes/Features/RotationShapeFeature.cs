using RimWorld;
using Verse;

namespace PlanningExtended.Shapes.Features
{
    public class RotationShapeFeature : BaseShapeFeature
    {
        readonly Direction _availableRotations;

        public override KeyBindingDef KeyBindingLeft => KeyBindingDefOf.Designator_RotateLeft;

        public override KeyBindingDef KeyBindingRight => KeyBindingDefOf.Designator_RotateRight;

        public override ShapeDisplayOptions DisplayOptions => ShapeDisplayOptions.Rotation;

        public override ShapeOptions ShapeOptions => ShapeOptions.Rotation;

        Direction _rotation;
        public Direction Rotation
        {
            get => _rotation;
            private set
            {
                if (_rotation == value)
                    return;
                
                _rotation = value;
                RequestUpdate();
            }
        }

        public RotationShapeFeature()
            : this(Direction.None, Direction.None)
        {
        }

        public RotationShapeFeature(Direction rotation, Direction availableRotations)
        {
            _availableRotations = availableRotations;
            Rotation = rotation;
        }

        public override void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptions == ShapeOptions)
                Rotation = GetAdjustedRotation(shapeOptionDirection, Rotation);
        }

        Direction GetAdjustedRotation(ShapeOptionDirection shapeOptionDirection, Direction currentRotation)
        {
            return shapeOptionDirection switch
            {
                ShapeOptionDirection.Left => currentRotation.GetNextCounterClockwise(_availableRotations),
                ShapeOptionDirection.Right => currentRotation.GetNextClockwise(_availableRotations),
                _ => currentRotation
            };
        }
    }
}
