using System.Collections.Generic;

namespace PlanningExtended.Shapes
{
    public class ShapesManager
    {
        readonly Dictionary<Shape, BaseShape> _shapes = new();

        //public static List<Shape> AvailableShapes = new(new[] { Shape.Fixed, Shape.Line, Shape.Rectangle, Shape.Ellipse });
        public static List<Shape> AvailableShapes = new(new[] { Shape.Line, Shape.Rectangle });

        public ShapesManager()
        {
            //_shapes.Add(Shape.Fixed, new FixedShape());
            _shapes.Add(Shape.Line, new LineShape());
            _shapes.Add(Shape.Rectangle, new RectangleShape());
            //_shapes.Add(Shape.Ellipse, new EllipseShape());
        }

        public BaseShape GetShape(Shape shape)
        {
            if (_shapes.TryGetValue(shape, out BaseShape baseShape))
                return baseShape;

            throw new KeyNotFoundException();
        }
    }
}
