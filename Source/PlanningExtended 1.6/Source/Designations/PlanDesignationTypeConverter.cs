using System;

namespace PlanningExtended.Designations
{
    public static class PlanDesignationTypeConverter
    {
        public static PlanDesignationType Convert(string defName)
        {
            return defName switch
            {
                "PlanDoors" => PlanDesignationType.PlanDoors,
                "PlanDoorsColored" => PlanDesignationType.PlanDoorsColored,
                "PlanObjects" => PlanDesignationType.PlanObjects,
                "PlanObjectsColored" => PlanDesignationType.PlanObjectsColored,
                "PlanFloors" => PlanDesignationType.PlanFloors,
                "PlanFloorsColored" => PlanDesignationType.PlanFloorsColored,
                "PlanWalls" => PlanDesignationType.PlanWall,
                "PlanWallsColored" => PlanDesignationType.PlanWallColored,
                _ => PlanDesignationType.Unknown
            };
        }

        public static string Convert(PlanDesignationType type)
        {
            return type switch
            {
                PlanDesignationType.PlanDoors => PlanDesignationType.PlanDoors.ToString(),
                PlanDesignationType.PlanDoorsColored => PlanDesignationType.PlanDoorsColored.ToString(),
                PlanDesignationType.PlanObjects => PlanDesignationType.PlanObjects.ToString(),
                PlanDesignationType.PlanObjectsColored => PlanDesignationType.PlanObjectsColored.ToString(),
                PlanDesignationType.PlanFloors => PlanDesignationType.PlanFloors.ToString(),
                PlanDesignationType.PlanFloorsColored => PlanDesignationType.PlanFloorsColored.ToString(),
                PlanDesignationType.PlanWall => PlanDesignationType.PlanWall.ToString(),
                PlanDesignationType.PlanWallColored => PlanDesignationType.PlanWallColored.ToString(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
