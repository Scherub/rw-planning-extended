using System.Collections.Generic;
using System.Linq;
using Verse;

namespace PlanningExtended.Updates
{
    internal static class PlanUpdateManager
    {
        static readonly List<BaseUpdate> updates = new BaseUpdate[]
        {
            new UpdateDesignationsToPlanDesignations(),
            new RotateDoorsUpdate()
        }.ToList();

        public static void ApplyUpdates()
        {
            Map map = Find.CurrentMap;

            if (map == null)
                return;

            ApplyUpdates(map, 0);
        }

        public static int ApplyUpdates(Map map, int lastUpdate)
        {
            int currentVersion = 0;

            foreach (BaseUpdate update in updates.OrderBy(u => u.Version))
            {
                if (update.Version > lastUpdate)
                {
                    if (!update.Apply(map))
                        break;

                    currentVersion = update.Version;
                }
            }

            return currentVersion;
        }
    }
}
