using PlanningExtended.Cells;
using PlanningExtended.Shapes.Plotter;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class EllipseGenerator : BaseShapeGenerator
    {
        public EllipseGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            AddValidCells(EllipsePlotter.PlotEllipse(areaDimensions.CenterX, areaDimensions.CenterZ, areaDimensions.Width, areaDimensions.Height));

            if (FillArea)
                DrawAreaFilling(ValidCells);
        }
    }
}
