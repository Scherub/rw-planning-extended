using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PlanningExtended.Designations;
using RimWorld;
using Verse;

namespace PlanningExtended.Plans.Converters
{
    internal static class MorePlanningConverter
    {
        static readonly Dictionary<int, ColorDef> colorMapping = new()
        {
            { 0, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_GrayLight", ColorDefinitions.NonColoredDef) },
            { 1, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_Blue", ColorDefinitions.NonColoredDef) },
            { 2, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_Viridian", ColorDefinitions.NonColoredDef) },
            { 3, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_Scarlet", ColorDefinitions.NonColoredDef) },
            { 4, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_YellowPastel", ColorDefinitions.NonColoredDef) },
            { 5, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_Pink", ColorDefinitions.NonColoredDef) },
            { 6, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_BluePastel", ColorDefinitions.NonColoredDef) },
            { 7, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_PurpleDeep", ColorDefinitions.NonColoredDef) },
            { 8, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_OrangePastel", ColorDefinitions.NonColoredDef) },
            { 9, ColorDefinitions.ColorDefs.FirstOrFallback(cd => cd.defName == "Structure_Black", ColorDefinitions.NonColoredDef) },
        };

        public static void Convert()
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            FieldInfo fieldInfo = null;

            DesignationDef wallPlanDesignation = PlanningDesignationDefOf.PlanWalls;

            foreach (IntVec3 position in map.AllCells)
            {
                List<Designation> designations = map.designationManager.AllDesignationsAt(position);

                Designation designation = designations.FirstOrDefault(d => d.def.defName == "MP_Plan");

                if (designation == null)
                    continue;

                fieldInfo ??= designation.GetType().GetField("Color", BindingFlags.Public | BindingFlags.Instance);

                ColorDef colorDef = ColorDefinitions.NonColoredDef;

                if (fieldInfo != null)
                {
                    int color = (int)fieldInfo.GetValue(designation);

                    colorDef = colorMapping.TryGetValue(color, ColorDefinitions.NonColoredDef);
                }

                designation.Delete();

                PlanDesignationPlacerUtilities.Designate(map, position, wallPlanDesignation, colorDef);
            }
        }
    }
}
