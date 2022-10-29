using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Fixed
{
    public class FixedOrbitalTradingStationhapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.FixedOrbitalTradingStation;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
