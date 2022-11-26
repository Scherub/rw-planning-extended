using PlanningExtended.Defs;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Updates
{
    internal class RotateDoorsUpdate : BaseUpdate
    {
        public override int Version => 2;

        protected override void OnUpdate(Map map)
        {
            DesignationDefContainer designationDefContainer = PlanningDesignationDefOf.DesignationDefs.FirstOrDefault(dd => dd.Type == PlanDesignationType.PlanDoors);

            foreach (DesignationDef designationDef in designationDefContainer.DesignationDefs)
                foreach (Designation designation in map.designationManager.designationsByDef[designationDef])
                    PlanDesignationPlacerUtilities.Update(map, designation.target.Cell);
        }
    }
}
