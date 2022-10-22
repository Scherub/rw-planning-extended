using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class PlanDoorsDesignator : BaseAddPlanDesignator
    {
        protected override DesignationDef Designation => DesignationDefOf.PlanDoors;
        
        protected override DesignationDef ColoredDesignation => DesignationDefOf.PlanDoorsColored;

        public PlanDoorsDesignator()
            : base("PlanDoors")
        {
            hotKey = KeyBindingDefOf.Misc9;
        }
    }
}
