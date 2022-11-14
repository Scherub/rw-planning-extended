using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanFloorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanFloors;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanFloorsColored;

        protected override PlanDesignationType PlanDesignationType => PlanDesignationType.PlanFloors;

        public PlanFloorsDesignator()
            : base("PlanFloors")
        {
            hotKey = KeyBindingDefOf.Misc12;
        }
    }
}
