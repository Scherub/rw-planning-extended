using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanWallsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => DesignationDefOf.PlanWalls;

        protected override DesignationDef ColoredDesignation => DesignationDefOf.PlanWallsColored;

        public PlanWallsDesignator()
            : base("PlanWalls")
        {
            hotKey = KeyBindingDefOf.Misc6;
            SelectShape(Shape.Rectangle);
        }
    }
}
