using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class BaseClickDesignator : Designator
    {
        public override int DraggableDimensions => 0;

        public override bool DragDrawMeasurements => false;

        public override bool DragDrawOutline => false;

        protected BaseClickDesignator(string name)
        {
            defaultLabel = $"PlanningExtended.{name}.Label".Translate();
            defaultDesc = $"PlanningExtended.{name}.Desc".Translate();
            icon = ContentFinder<Texture2D>.Get($"UI/Designators/{name}", true);

            soundSucceeded = SoundDefOf.Designate_PlanAdd;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return false;
        }
    }
}
