using System.Linq;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanObjectsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => DesignationDefOf.PlanObjects;

        protected override DesignationDef ColoredDesignation => DesignationDefOf.PlanObjectsColored;

        public PlanObjectsDesignator()
            : base("PlanObjects")
        {
            hotKey = KeyBindingDefOf.Misc11;
        }
    }
}
