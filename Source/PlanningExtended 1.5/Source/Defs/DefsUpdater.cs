namespace PlanningExtended.Defs
{
    public static class DefsUpdater
    {
        public static void UpdateDefs()
        {
            bool areDesignationsPersistent = PlanningMod.Settings.General.areDesignationsPersistent;

            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                designationDefContainer.Default.removeIfBuildingDespawned = !areDesignationsPersistent;
                designationDefContainer.Colored.removeIfBuildingDespawned = !areDesignationsPersistent;
            }
        }
    }
}
