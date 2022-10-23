using System.Linq;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanObjectsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanObjects;

        protected override DesignationDef ColoredDesignation => PlanningDesignationDefOf.PlanObjectsColored;

        public PlanObjectsDesignator()
            : base("PlanObjects")
        {
            hotKey = KeyBindingDefOf.Misc11;
        }
    }
}
