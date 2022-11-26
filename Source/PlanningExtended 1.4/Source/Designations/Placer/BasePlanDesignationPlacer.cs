using RimWorld;
using Verse;

namespace PlanningExtended.Designations.Placer
{
    internal abstract class BasePlanDesignationPlacer
    {
        protected abstract PlanDesignationType Type { get; }

        public void Designate(Map map, IntVec3 position, DesignationDef designationDef, ColorDef colorDef)
        {
            bool removedPlanDesignation = map.designationManager.RemovePlanDesignationsAt(position);

            RotationDirection rotation = GetRotation(map, position);

            map.designationManager.AddDesignation(new PlanDesignation(Type, position, designationDef, colorDef, rotation));

            OnPostDesignate(map, position, removedPlanDesignation);
        }

        public void Update(Map map, IntVec3 position)
        {
            PlanDesignation planDesignation = map.designationManager.GetOnlyPlanDesignationAt(position);

            planDesignation.Rotation = GetRotation(map, position);
        }

        protected virtual RotationDirection GetRotation(Map map, IntVec3 position)
        {
            return RotationDirection.None;
        }

        protected virtual void OnPostDesignate(Map map, IntVec3 position, bool removedPlanDesignation)
        {

        }
    }
}
