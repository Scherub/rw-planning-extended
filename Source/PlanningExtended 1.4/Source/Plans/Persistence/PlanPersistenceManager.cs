using System.IO;
using System;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace PlanningExtended.Plans.Persistence
{
    public static class PlanPersistenceManager
    {
        public static string PlanFolderPath => Path.Combine(GenFilePaths.SaveDataFolderPath, "Planning");

        public static string GetAbsPathForPlanInfo(string planInfoName)
        {
            return Path.Combine(PlanFolderPath, planInfoName + ".pln");
        }

        public static IEnumerable<FileInfo> GetAllPlanInfoFiles()
        {
            DirectoryInfo directoryInfo = new(PlanFolderPath);
            
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            
            return from f in directoryInfo.GetFiles()
                   where f.Extension == ".pln"
                   orderby f.Name
                   select f;
        }

        public static void Save(PlanInfo planInfo, string fileName)
        {
            try
            {
                CreateDirectoryIfRequired();

                string absFilePath = GetAbsPathForPlanInfo(fileName);

                SafeSaver.Save(absFilePath, "savedPlanInfo", delegate
                {
                    Scribe_Deep.Look(ref planInfo, "planInfo");
                }, false);
            }
            catch (Exception ex)
            {
                Log.Error("Exception while saving planInfo: " + ex.ToString());
            }
        }

        public static bool Load(string fileName, out PlanInfo planInfo)
        {
            planInfo = null;

            try
            {
                string absFilePath = GetAbsPathForPlanInfo(fileName);

                Scribe.loader.InitLoading(absFilePath);

                Scribe_Deep.Look(ref planInfo, "planInfo");
                
                Scribe.loader.FinalizeLoading();
            }
            catch (Exception ex)
            {
                Log.Error("Exception loading planInfo: " + ex.ToString());
                Scribe.ForceStop();
            }

            return planInfo != null;
        }

        static void CreateDirectoryIfRequired()
        {
            if (!Directory.Exists(PlanFolderPath))
                Directory.CreateDirectory(PlanFolderPath);
        }
    }
}
