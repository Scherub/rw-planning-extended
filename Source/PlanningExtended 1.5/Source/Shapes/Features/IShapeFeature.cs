namespace PlanningExtended.Shapes.Features
{
    public interface IShapeFeature
    {
        ShapeDisplayOptions DisplayOptions { get; }

        bool RequiresUpdate { get; }

        void HandleKeyboardInput();

        void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection);

        void HandledUpdate();
    }
}
