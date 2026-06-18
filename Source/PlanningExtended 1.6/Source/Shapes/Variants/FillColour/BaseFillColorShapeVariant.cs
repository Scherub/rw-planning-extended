using System.Collections.Generic;
using PlanningExtended.Defs;
using PlanningExtended.Designations;
using PlanningExtended.Shapes.Variants.Fill;
using RimWorld;
using Verse;

namespace PlanningExtended.Shapes.Variants.FillColor;

internal abstract class BaseFillColorShapeVariant : BaseFloodFillShapeVariant
{
    public abstract bool MatchPlanType { get; }

    public override HashSet<IntVec3> ComputeFillCells(Map map, IntVec3 startCell)
    {
        PlanDesignation startDesignation = map.designationManager.GetOnlyPlanDesignationAt(startCell);

        if (startDesignation == null)
            return [];

        ColorDef sourceColorDef = startDesignation.colorDef;

        PlanDesignationType sourcePlanType = MatchPlanType
            ? DesignationDefUtilities.GetType(startDesignation.def)
            : PlanDesignationType.Unknown;

        return FloodFill(map, startCell, c =>
        {
            PlanDesignation designation = map.designationManager.GetOnlyPlanDesignationAt(c);

            if (designation == null || designation.colorDef != sourceColorDef)
                return false;

            if (sourcePlanType != PlanDesignationType.Unknown && DesignationDefUtilities.GetType(designation.def) != sourcePlanType)
                return false;

            return true;
        });
    }
}
