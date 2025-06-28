using RimWorld;
using Verse;

namespace PlanningExtended.Shapes.Features
{
    public class ThicknessShapeFeature : BaseShapeFeature
    {
        readonly int _maxThickness;

        public override KeyBindingDef KeyBindingLeft => KeyBindingDefOf.Designator_RotateLeft;

        public override KeyBindingDef KeyBindingRight => KeyBindingDefOf.Designator_RotateRight;

        public override ShapeDisplayOptions DisplayOptions => ShapeDisplayOptions.Thickness;

        public override ShapeOptions ShapeOptions => ShapeOptions.Thickness;

        int _thickness = 1;
        public int Thickness
        {
            get => _thickness;
            private set
            {
                if (_thickness == value)
                    return;

                _thickness = value;
                RequestUpdate();
            }
        }

        public ThicknessShapeFeature()
            : this(3)
        {
        }

        public ThicknessShapeFeature(int maxThickness)
        {
            _maxThickness = maxThickness;
        }

        public override void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {
            if (shapeOptions == ShapeOptions)
                Thickness = GetAdjustedThickness(shapeOptionDirection, Thickness);
        }

        int GetAdjustedThickness(ShapeOptionDirection shapeOptionDirection, int thickness)
        {
            return shapeOptionDirection switch
            {
                ShapeOptionDirection.Left => thickness > 1 ? --thickness : thickness,
                ShapeOptionDirection.Right => thickness < _maxThickness ? ++thickness : thickness,
                _ => thickness
            };
        }
    }
}
