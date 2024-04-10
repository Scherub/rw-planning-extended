using System;

namespace PlanningExtended
{
    [Flags]
    public enum ShapeDisplayOptions
    {
        None = 0,

        DisplayVariant = 0x01,

        Rotation = 0x02,

        NumberOfSegments = 0x04,

        Thickness = 0x08,
    }
}
