using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Fixed
{
    public class FixedMaxRoomSizeShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.FixedMaxRoomSize;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
