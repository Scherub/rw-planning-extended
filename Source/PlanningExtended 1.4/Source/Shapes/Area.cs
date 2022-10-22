using PlanningExtended.Cells;
using PlanningExtended.Shapes.Options;
using Verse;

namespace PlanningExtended.Shapes
{
    public class Area : BaseShape<NullShapeOptions>
    {
        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }

        public Area()
            : base()
        {

        }
    }
}
