﻿using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions
{
    public abstract class BaseShapeDimensionsModifier
    {
        public abstract AreaDimensions Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation);
    }
}
