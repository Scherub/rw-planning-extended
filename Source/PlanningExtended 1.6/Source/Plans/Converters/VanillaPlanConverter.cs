using System.Linq;
using PlanningExtended.Designations;
using RimWorld;
using Verse;

namespace PlanningExtended.Plans.Converters
{
    internal static class VanillaPlanConverter
    {
        public static void Convert()
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            DesignationDef wallPlanDesignation = PlanningDesignationDefOf.PlanWalls;

            foreach (Plan plan in map.planManager.AllPlans.ToList())
            {
                ColorDef colorDef = GetMatchingColorDef(plan.Color);

                foreach (IntVec3 cell in plan.Cells)
                    PlanDesignationPlacerUtilities.Designate(map, cell, wallPlanDesignation, colorDef);

                plan.Delete();
            }
        }

        static ColorDef GetMatchingColorDef(ColorDef color)
        {
            ColorDef colorDef = ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == color.defName);

            if (colorDef != null)
                return colorDef;

            colorDef = (color.defName) switch
            {
                "PlanGray" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_GrayLight"),
                "PlanRed" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_Scarlet"),
                "PlanOrange" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_OrangePastel"),
                "PlanYellow" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_YellowPastel"),
                "PlanGreen" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_GreenPastel"),
                "PlanCyan" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_BlueIce"),
                "PlanBlue" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_Blue"),
                "PlanPurple" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_PurpleDeep"),
                "PlanPink" => ColorDefinitions.ColorDefs.FirstOrDefault(cd => cd.defName == "Structure_Pink"),
                _ => ColorDefinitions.NonColoredDef
            };

            if (colorDef == null)
                colorDef = ColorDefinitions.NonColoredDef;

            return colorDef;
        }
    }
}
