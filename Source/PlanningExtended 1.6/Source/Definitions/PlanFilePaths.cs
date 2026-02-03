using System.IO;
using Verse;

namespace PlanningExtended;

internal class PlanFilePaths
{
    public static string PlanFolderPath => Path.Combine(GenFilePaths.SaveDataFolderPath, "Planning");
}
