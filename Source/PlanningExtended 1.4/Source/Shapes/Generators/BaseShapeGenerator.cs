using System;
using System.Collections.Generic;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Generators
{
    internal abstract class BaseShapeGenerator
    {
        AreaDimensions _areaDimensions;

        bool _requiresUpdate;

        readonly HashSet<IntVec3> _validCells = new();

        public HashSet<IntVec3> Update(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            // TODO: implement equals
            if (!_requiresUpdate && areaDimensions == _areaDimensions)
                return _validCells;

            _areaDimensions = areaDimensions;

            //AreaDimensions actualAreaDimensions = ApplyModifierToAreaDimensions(areaDimensions, mousePosition);

            OnPreUpdate(areaDimensions);

            _validCells.Clear();

            OnUpdate(areaDimensions, mousePosition);

            _requiresUpdate = false;

            return _validCells;
        }

        public bool IsCellValid(IntVec3 cell)
        {
            return _validCells.Contains(cell);
        }

        //protected virtual AreaDimensions ApplyModifierToAreaDimensions(AreaDimensions areaDimensions, IntVec3 mousePosition, bool applyModifier)
        //{
        //    int minX = areaDimensions.MinX;
        //    int minZ = areaDimensions.MinZ;
        //    int maxX = areaDimensions.MaxX;
        //    int maxZ = areaDimensions.MaxZ;

        //    if (applyModifier)
        //    {
        //        int length = Math.Min(areaDimensions.Width, areaDimensions.Height);

        //        //IntVec3 startPosition = areaDimensions.GetStartPosition(mousePosition);

        //        maxX = areaDimensions.MinX + length;
        //        minZ = areaDimensions.MaxZ - length;
        //    }

        //    return new(minX, minZ, maxX, maxZ);
        //}

        protected virtual void OnPreUpdate(AreaDimensions areaDimensions)
        {

        }

        protected abstract void OnUpdate(AreaDimensions areaDimensions, IntVec3 mousePosition);

        protected void RequiresUpdate()
        {
            _requiresUpdate = true;
        }

        protected void AddValidCell(int x, int z)
        {
            _validCells.Add(new IntVec3(x, 0, z));
        }
    }
}
