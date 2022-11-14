using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanObjectsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanObjects;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanObjectsColored;

        protected override PlanDesignationType PlanDesignationType => PlanDesignationType.PlanObjects;
        
        public PlanObjectsDesignator()
            : base("PlanObjects")
        {
            hotKey = KeyBindingDefOf.Misc11;
        }
    }
}
