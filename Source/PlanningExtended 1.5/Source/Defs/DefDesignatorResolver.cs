using Verse;

namespace PlanningExtended.Defs
{
    internal static class DefDesignatorResolver
    {
        public static TDesignator Resolve<TDesignator>()
            where TDesignator : Designator
        {
            DesignationCategoryDef designationCategoryDef = DefDatabase<DesignationCategoryDef>.GetNamed("PlanningExtended");

            return designationCategoryDef.AllResolvedDesignators.FirstOrDefault(d => d.GetType() == typeof(TDesignator)) as TDesignator;

            //foreach (Designator designator in designationCategoryDef.AllResolvedDesignators)
            //{

            //}
        }
    }
}
