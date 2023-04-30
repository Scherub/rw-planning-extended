using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Shapes.Plotter;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal abstract class BasePolygonGenerator : BaseShapeGenerator
    {
        protected bool FillArea { get; }

        protected abstract List<LineIndex> LineIndices { get; }

        protected BasePolygonGenerator(bool fillArea)
        {
            FillArea = fillArea;
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier)
        {
            List<IntVec3> vertices = GetVertices(areaDimensions, mousePosition, applyShapeDimensionsModifier);

            for (int i = 0; i < LineIndices.Count; i++)
            {
                LineIndex lineIndex = LineIndices[i];

                AddValidCells(LinePlotter.PlotLine(vertices[lineIndex.Start], vertices[lineIndex.End]));
            }
        }

        protected abstract List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyShapeDimensionsModifier);

        protected struct LineIndex
        {
            public int Start;
            
            public int End;

            public LineIndex(int start, int end)
            {
                Start = start;
                End = end;
            }
        }
    }
}
