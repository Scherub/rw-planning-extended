using System.Collections.Generic;

namespace PlanningExtended.Shapes
{
    public class ShapesManager
    {
        readonly Dictionary<Shape, BaseShape> _shapes = new();

        public static List<Shape> AvailableShapes = new(new[] { Shape.Area, Shape.Rectangle, Shape.Cross });

        public ShapesManager()
        {
            _shapes.Add(Shape.Area, new Area());
            _shapes.Add(Shape.Rectangle, new Rectangle());
            //_shapes.Add(Shape.SubdivdedRectangle, new SubdividedRectangle());
            _shapes.Add(Shape.Cross, new CrossShape());
        }

        public BaseShape GetShape(Shape shape)
        {
            if (_shapes.TryGetValue(shape, out BaseShape baseShape))
                return baseShape;

            throw new KeyNotFoundException();
        }
    }
}
