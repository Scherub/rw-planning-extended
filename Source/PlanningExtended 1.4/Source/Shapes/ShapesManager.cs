using System.Collections.Generic;

namespace PlanningExtended.Shapes
{
    internal class ShapesManager
    {
        readonly Dictionary<Shape, BaseShape> _shapes = new();

        public static List<Shape> AvailableShapes = new() { /*Shape.Fixed, */Shape.Point, Shape.Line, Shape.Triangle, Shape.Quadrangle, Shape.Hexagon, Shape.Octagon/*, Shape.Ellipse*/ };

        public ShapesManager()
        {
            //_shapes.Add(Shape.Fixed, new FixedShape());
            _shapes.Add(Shape.Point, new PointShape());
            _shapes.Add(Shape.Line, new LineShape());
            _shapes.Add(Shape.Triangle, new TriangleShape());
            _shapes.Add(Shape.Quadrangle, new QuadrangleShape());
            _shapes.Add(Shape.Hexagon, new HexagonShape());
            _shapes.Add(Shape.Octagon, new OctagonShape());
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
