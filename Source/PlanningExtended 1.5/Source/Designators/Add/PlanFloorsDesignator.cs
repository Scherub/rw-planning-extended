using Verse;

namespace PlanningExtended.Designators
{
    public class PlanFloorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanFloors;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanFloorsColored;

        public override PlanDesignationType PlanDesignationType => PlanDesignationType.PlanFloors;

        public PlanFloorsDesignator()
            : base("PlanFloors")
        {
            hotKey = PlanningKeyBindingDefOf.Planning_PlanFloors;
        }
    }
}
