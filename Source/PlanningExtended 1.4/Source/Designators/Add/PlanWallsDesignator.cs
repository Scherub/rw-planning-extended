using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanWallsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanWalls;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanWallsColored;

        public PlanWallsDesignator()
            : base("PlanWalls")
        {
            hotKey = KeyBindingDefOf.Misc6;
            SelectedShape.SelectShapeVariant(ShapeVariant.OpenRectangle);
        }
    }
}
