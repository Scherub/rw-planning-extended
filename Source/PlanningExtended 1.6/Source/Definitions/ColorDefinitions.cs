using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace PlanningExtended;

public static class ColorDefinitions
{
    public const string DefaultColorName = "Structure_White";

    public static readonly List<ColorDef> ColorDefs =
    [
        .. DefDatabase<ColorDef>.AllDefs.Where(cd => cd.defName.EqualsIgnoreCase(DefaultColorName)),
        .. DefDatabase<ColorDef>.AllDefs.Where(cd => cd.colorType == ColorType.Structure && !cd.defName.EqualsIgnoreCase(DefaultColorName)).OrderBy(cd => cd.displayOrder),
    ];

    public static readonly ColorDef NonColoredDef = DefDatabase<ColorDef>.AllDefs.FirstOrDefault(cd => cd.colorType == ColorType.Structure && cd.defName.EqualsIgnoreCase(DefaultColorName));
}
