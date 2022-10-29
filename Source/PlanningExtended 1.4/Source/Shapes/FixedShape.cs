using System.Collections.Generic;
using PlanningExtended.Shapes.Variants;
using PlanningExtended.Shapes.Variants.Fixed;

namespace PlanningExtended.Shapes
{
    public class FixedShape : BaseShape
    {
        public override List<BaseShapeVariant> ShapeVariants => new() { new FixedSunLampShapeVariant(), new FixedOrbitalTradingStationhapeVariant(), new FixedMaxRoomSizeShapeVariant() };

        public FixedShape()
            : base(ShapeVariant.FixedSunLamp)
        {
        }
    }
}
