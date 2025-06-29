using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Designations;
using RimWorld;
using Verse;

namespace PlanningExtended
{
    public static class DesignationManagerExtensions
    {
        public static bool HasPlanDesignationAt(this DesignationManager designationManager, IntVec3 position)
        {
            return designationManager.GetPlanDesignationAt(position) != null;
        }

        public static bool RemovePlanDesignationsAt(this DesignationManager designationManager, IntVec3 position)
        {
            List<Designation> designations = designationManager.AllDesignationsAt(position).Where(d => d is PlanDesignation || d.def == DesignationDefOf.Plan).ToList();

            bool hasPlanDesignations = designations.Count > 0;

            designations.ForEach(d => d.Delete());

            return hasPlanDesignations;
        }

        public static void RemoveDesignations(this DesignationManager designationManager, List<Designation> designations)
        {
            for (int i = designations.Count - 1; i >= 0; i--)
                designationManager.RemoveDesignation(designations[i]);
        }

        public static Designation GetPlanDesignationAt(this DesignationManager designationManager, IntVec3 position)
        {
            return designationManager.AllDesignationsAt(position).FirstOrDefault(d => d is PlanDesignation || d.def == DesignationDefOf.Plan);
        }

        public static PlanDesignation GetOnlyPlanDesignationAt(this DesignationManager designationManager, IntVec3 position)
        {
            return designationManager.AllDesignationsAt(position).FirstOrDefault(d => d is PlanDesignation) as PlanDesignation;
        }

        public static IEnumerable<PlanDesignation> GetOnlyPlanDesignationsAt(this DesignationManager designationManager, IntVec3[] positions)
        {
            foreach (IntVec3 position in positions)
            {
                PlanDesignation planDesignation = designationManager.GetOnlyPlanDesignationAt(position);

                if (planDesignation != null)
                    yield return planDesignation;
            }
        }

        public static bool IsPlanDesignationOfTypeAt(this DesignationManager designationManager, PlanDesignationType planDesignationType, IntVec3 position)
        {
            return designationManager.AllDesignationsAt(position).Any(d => d is PlanDesignation planDesignation && planDesignation.PlanType == planDesignationType);
        }

        public static bool HasPlanDesignationsOfTypeAt(this DesignationManager designationManager, PlanDesignationType planDesignationType, IntVec3[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
                if (!designationManager.IsPlanDesignationOfTypeAt(planDesignationType, positions[i]))
                    return false;

            return true;
        }
    }
}
