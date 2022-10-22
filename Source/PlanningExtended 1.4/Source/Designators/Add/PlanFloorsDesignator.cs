using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanFloorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => DesignationDefOf.PlanFloors;

        protected override DesignationDef ColoredDesignation => DesignationDefOf.PlanFloorsColored;

        public PlanFloorsDesignator()
            : base("PlanFloors")
        {
            hotKey = KeyBindingDefOf.Misc12;
        }
    }
}
