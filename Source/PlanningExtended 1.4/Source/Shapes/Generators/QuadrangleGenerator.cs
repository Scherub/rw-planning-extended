using System.Collections.Generic;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal class QuadrangleGenerator : BasePolygonGenerator
    {
        readonly List<LineIndex> _indices = new()
        {
            new LineIndex(0, 1),
            new LineIndex(1, 2),
            new LineIndex(2, 3),
            new LineIndex(3, 0)
        };

        protected override List<LineIndex> LineIndices => _indices;

        public QuadrangleGenerator(bool fillArea)
            : base(fillArea)
        {
        }

        protected override void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            if (FillArea && rotation == Direction.Horizontal)
                DrawFilledRectangle(areaDimensions);
            else
                base.OnUpdate(areaDimensions, mousePosition, rotation, applyShapeDimensionsModifier);
        }

        protected override List<IntVec3> GetVertices(AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation, bool applyShapeDimensionsModifier)
        {
            return rotation switch
            {
                Direction.Diagonal => GetVerticesDirectionDiagonal(areaDimensions),
                _ => GetVerticesDirectionHorizontal(areaDimensions),
            };
        }

        protected void DrawFilledRectangle(AreaDimensions areaDimensions)
        {
            HashSet<IntVec3> vertices = new();

            for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
                for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                    vertices.Add(new IntVec3(x, 0, z));

            AddValidCells(vertices);
        }

        protected List<IntVec3> GetVerticesDirectionHorizontal(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MinZ),
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MinZ)
            };
        }

        protected virtual List<IntVec3> GetVerticesDirectionDiagonal(AreaDimensions areaDimensions)
        {
            return new List<IntVec3>
            {
                new IntVec3(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                new IntVec3(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MinZ)
            };
        }
    }
}
