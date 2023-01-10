using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class NullShapeModifier : BaseShapeModifier
    {
        public override AreaDimensions Update(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            return areaDimensions;
        }
    }
}
