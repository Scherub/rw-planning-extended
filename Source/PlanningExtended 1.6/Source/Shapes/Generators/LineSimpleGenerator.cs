﻿using PlanningExtended.Cells;
using PlanningExtended.Shapes.Plotter;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class LineSimpleGenerator : BaseShapeGenerator
    {
        public LineSimpleGenerator()
            : base(false)
        {
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            IntVec3 endPosition = new(areaDimensions.Width == 1 ? areaDimensions.MinX : mousePosition.x, 0, areaDimensions.Height == 1 ? areaDimensions.MinZ : mousePosition.z);
            IntVec3 startPosition = areaDimensions.GetStartPosition(endPosition);

            AddValidCells(LinePlotter.PlotLine(startPosition, endPosition));
        }
    }
}
