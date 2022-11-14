using System.Collections.Generic;
using PlanningExtended.Shapes.Variants;
using PlanningExtended.Shapes.Variants.Rectangles;

namespace PlanningExtended.Shapes
{
    public class RectangleShape : BaseShape
    {
        public override List<BaseShapeVariant> ShapeVariants => new() { new OpenRectangleShapeVariant(), /* new GridRectangleShapeVariant(), */ new FilledRectangleShapeVariant() };

        public RectangleShape()
            : base(ShapeVariant.FilledRectangle)
        {
        }
    }
}
