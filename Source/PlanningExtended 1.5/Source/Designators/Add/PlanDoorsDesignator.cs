using Verse;

namespace PlanningExtended.Designators
{
    public class PlanDoorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanDoors;
        
        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanDoorsColored;

        public override PlanDesignationType PlanDesignationType => PlanDesignationType.PlanDoors;

        public PlanDoorsDesignator()
            : base("PlanDoors")
        {
            hotKey = PlanningKeyBindingDefOf.Planning_PlanDoors;
        }
    }
}
