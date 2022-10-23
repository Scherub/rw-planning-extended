using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanFloorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanFloors;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanFloorsColored;

        public PlanFloorsDesignator()
            : base("PlanFloors")
        {
            hotKey = KeyBindingDefOf.Misc12;
        }
    }
}
