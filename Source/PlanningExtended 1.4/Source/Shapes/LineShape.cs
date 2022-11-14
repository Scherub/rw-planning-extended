using System.Collections.Generic;
using PlanningExtended.Shapes.Variants;
using PlanningExtended.Shapes.Variants.Lines;

namespace PlanningExtended.Shapes
{
    public class LineShape : BaseShape
    {
        public override List<BaseShapeVariant> ShapeVariants => new() { /* new SimpleLineShapeVariant(), */ new TwoLinesShapeVariant() };

        public LineShape()
            : base(ShapeVariant.TwoLines)
        {
        }
    }
}
