namespace PlanningExtended.Defs
{
    public static class DefsUpdater
    {
        public static void UpdateDefs()
        {
            bool areDesignationsPersistent = PlanningMod.Settings.areDesignationsPersistent;

            foreach (var designationDef in PlanningDesignationDefOf.DesignationDefs)
                designationDef.removeIfBuildingDespawned = !areDesignationsPersistent;
        }
    }
}
