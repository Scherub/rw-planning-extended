using System;

namespace PlanningExtended
{
    [Flags]
    public enum ShapeDisplayOptions
    {
        None = 0,

        DisplayVariant = 0x01,

        NumberOfSegments = 0x02,
    }
}
