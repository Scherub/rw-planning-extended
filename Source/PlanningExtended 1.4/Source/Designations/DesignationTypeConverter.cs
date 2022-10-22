using System;

namespace PlanningExtended.Designations
{
    public static class PlanDesignitionTypeConverter
    {
        public static PlanDesignitionType Convert(string defName)
        {
            return defName switch
            {
                "PlanDoors" => PlanDesignitionType.PlanDoors,
                "PlanDoorsColored" => PlanDesignitionType.PlanDoorsColored,
                "PlanObjects" => PlanDesignitionType.PlanObjects,
                "PlanObjectsColored" => PlanDesignitionType.PlanObjectsColored,
                "PlanFloors" => PlanDesignitionType.PlanFloors,
                "PlanFloorsColored" => PlanDesignitionType.PlanFloorsColored,
                "PlanWallsColored" => PlanDesignitionType.PlanWallColored,
                _ => PlanDesignitionType.PlanWall
            };
        }

        public static string Convert(PlanDesignitionType type)
        {
            return type switch
            {
                PlanDesignitionType.PlanDoors => PlanDesignitionType.PlanDoors.ToString(),
                PlanDesignitionType.PlanDoorsColored => PlanDesignitionType.PlanDoorsColored.ToString(),
                PlanDesignitionType.PlanObjects => PlanDesignitionType.PlanObjects.ToString(),
                PlanDesignitionType.PlanObjectsColored => PlanDesignitionType.PlanObjectsColored.ToString(),
                PlanDesignitionType.PlanFloors => PlanDesignitionType.PlanFloors.ToString(),
                PlanDesignitionType.PlanFloorsColored => PlanDesignitionType.PlanFloorsColored.ToString(),
                PlanDesignitionType.PlanWall => PlanDesignitionType.PlanWall.ToString(),
                PlanDesignitionType.PlanWallColored => PlanDesignitionType.PlanWallColored.ToString(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
