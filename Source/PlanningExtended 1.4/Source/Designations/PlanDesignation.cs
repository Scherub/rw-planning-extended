using PlanningExtended.Plans;
using RimWorld;
using Verse;

namespace PlanningExtended.Designations
{
    public class PlanDesignation : Designation
    {
        public PlanDesignation()
            : base()
        {

        }

        public PlanDesignation(LocalTargetInfo target, DesignationDef def, ColorDef colorDef = null)
            : base(target, def, colorDef)
        {

        }

        public override void DesignationDraw()
        {
            if (!PlanManager.ArePlansVisible)
                return;

            base.DesignationDraw();
        }
    }
}
