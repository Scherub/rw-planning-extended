using PlanningExtended.Cells;
using PlanningExtended.Shapes.Options;
using Verse;

namespace PlanningExtended.Shapes
{
    public abstract class BaseShape
    {
        public BaseShapeOptions Options { get; }

        public abstract bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions);
    }

    public abstract class BaseShape<TOptions> : BaseShape
        where TOptions : BaseShapeOptions, new()
    {
        public new TOptions Options { get; }

        protected BaseShape()
        {
            Options = new TOptions();
        }
    }
}
