using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Content
{
    public abstract class BaseShapeContentModifier
    {
        public abstract AreaDimensions Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation);
    }
}
