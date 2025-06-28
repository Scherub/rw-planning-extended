using System.Collections.Generic;

namespace PlanningExtended.Shapes
{
    internal class ShapesManager
    {
        readonly Dictionary<Shape, BaseShape> _shapes = new();

        public static List<Shape> AvailableShapes = new() { /*Shape.Fixed, */Shape.Point, Shape.Line, Shape.Triangle, Shape.Rectangle, Shape.Pentagon, Shape.Hexagon, Shape.Octagon, Shape.Ellipse };

        public ShapesManager()
        {
            //Register(new FixedShape());
            Register(new PointShape());
            Register(new LineShape());
            Register(new TriangleShape());
            Register(new RectangleShape());
            Register(new PentagonShape());
            Register(new HexagonShape());
            Register(new OctagonShape());
            Register(new EllipseShape());
        }

        public BaseShape GetShape(Shape shape)
        {
            if (_shapes.TryGetValue(shape, out BaseShape baseShape))
                return baseShape;

            throw new KeyNotFoundException();
        }

        void Register(BaseShape shape)
        {
            _shapes.Add(shape.Shape, shape);
        }
    }
}
