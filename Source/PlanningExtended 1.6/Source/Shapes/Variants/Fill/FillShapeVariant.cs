using System.Collections.Generic;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Shapes.Variants.Fill;

internal class FillShapeVariant : BaseFloodFillShapeVariant
{
    public override ShapeVariant ShapeVariant => ShapeVariant.Fill;

    public override HashSet<IntVec3> ComputeFillCells(Map map, IntVec3 startCell)
    {
        PlanDesignation startDesignation = map.designationManager.GetOnlyPlanDesignationAt(startCell);

        if (startDesignation != null)
            return [];

        return FloodFill(map, startCell, c =>
        {
            PlanDesignation designation = map.designationManager.GetOnlyPlanDesignationAt(c);

            return designation == null;
        });
    }
}
