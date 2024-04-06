using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseClickDesignator : Designator
    {
        public override int DraggableDimensions => 0;

        public override bool DragDrawMeasurements => false;

        public override bool DragDrawOutline => false;

        protected BaseClickDesignator(string name)
            : this(name, name)
        {
        }

        protected BaseClickDesignator(string name, string iconName)
        {
            defaultLabel = $"PlanningExtended.Designator.{name}.Label".Translate();
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate();
            icon = ContentFinder<Texture2D>.Get($"UI/Designators/{iconName}", true);

            soundSucceeded = SoundDefOf.Designate_PlanAdd;
            useMouseIcon = true;
        }

        public override void ProcessInput(Event ev)
        {
            Click();
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return false;
        }

        public abstract void Click();
    }
}
