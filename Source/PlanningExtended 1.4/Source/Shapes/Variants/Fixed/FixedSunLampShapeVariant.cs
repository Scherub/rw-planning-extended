using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Fixed
{
    public class FixedSunLampShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.FixedSunLamp;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
