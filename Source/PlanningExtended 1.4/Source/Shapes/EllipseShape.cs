using System.Collections.Generic;
using PlanningExtended.Shapes.Variants;
using PlanningExtended.Shapes.Variants.Ellipses;

namespace PlanningExtended.Shapes
{
    public class EllipseShape : BaseShape
    {
        public override List<BaseShapeVariant> ShapeVariants => new() { new OpenEllipseShapeVariant(), new FilledEllipseShapeVariant() };

        public EllipseShape()
            : base(ShapeVariant.OpenEllipse)
        {
        }
    }
}
