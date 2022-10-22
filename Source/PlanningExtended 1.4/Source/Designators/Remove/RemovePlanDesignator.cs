using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class RemovePlanDesignator : BaseShapePlanDesignator
    {
        protected override DesignationDef Designation => DesignationDefOf.PlanObjects;

        public override bool DragDrawOutline => true;

        protected override bool HasPopupMenu => false;

        public RemovePlanDesignator()
            : base("RemovePlan")
        {
            soundSucceeded = SoundDefOf.Designate_PlanRemove;
            hotKey = KeyBindingDefOf.Designator_Cancel;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c))
                return false;

            return Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            Map.designationManager.RemovePlanDesignationsAt(c);
        }
    }
}
