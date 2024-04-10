using Verse;

namespace PlanningExtended.Shapes.Features
{
    public abstract class BaseShapeFeature : IShapeFeature
    {
        public abstract KeyBindingDef KeyBindingLeft { get; }

        public abstract KeyBindingDef KeyBindingRight { get; }

        public abstract ShapeDisplayOptions DisplayOptions { get; }

        public abstract ShapeOptions ShapeOptions { get; }

        public bool RequiresUpdate { get; private set; }

        public virtual void HandleKeyboardInput()
        {
            if (KeyBindingLeft?.KeyDownEvent == true)
                ChangeShapeOption(ShapeOptions, ShapeOptionDirection.Left);
            else if (KeyBindingRight?.KeyDownEvent == true)
                ChangeShapeOption(ShapeOptions, ShapeOptionDirection.Right);
        }

        public abstract void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection);

        protected void RequestUpdate()
        {
            RequiresUpdate = true;
        }

        public void HandledUpdate()
        {
            RequiresUpdate = false;
        }
    }
}
