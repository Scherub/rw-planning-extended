using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    public abstract class BaseShapeModifier
    {
        public abstract AreaDimensions Update(AreaDimensions areaDimensions, IntVec3 mousePosition);
    }
}
