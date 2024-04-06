using Verse;

namespace PlanningExtended.Designators
{
    public class PlanObjectsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanObjects;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanObjectsColored;

        public override PlanDesignationType PlanDesignationType => PlanDesignationType.PlanObjects;
        
        public PlanObjectsDesignator()
            : base("PlanObjects")
        {
            hotKey = PlanningKeyBindingDefOf.Planning_PlanObjects;
        }
    }
}
