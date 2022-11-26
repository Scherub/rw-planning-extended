using System.Linq;
using PlanningExtended.Designations;
using Verse;

namespace PlanningExtended.Updates
{
    internal class UpdateDesignationsToPlanDesignations : BaseUpdate
    {
        public override int Version => 1;

        protected override void OnUpdate(Map map)
        {
            foreach (IntVec3 cell in map.AllCells)
            {
                Designation designation = map.designationManager.AllDesignationsAt(cell).Where(d => d is Designation && PlanningDesignationDefOf.AllDesignationDefs.Contains(d.def)).FirstOrDefault();

                if (designation == null)
                    continue;

                map.designationManager.RemoveDesignation(designation);

                PlanDesignationPlacerUtilities.Designate(map, designation.target.Cell, designation.def, designation.colorDef);
            }
        }
    }
}
