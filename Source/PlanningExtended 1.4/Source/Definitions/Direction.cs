using System;

namespace PlanningExtended
{
    [Flags]
    public enum Direction
    {
        None,

        North = 0x01,

        NorthEast = 0x02,

        East = 0x04,

        SouthEast = 0x08,

        South = 0x10,

        SouthWest = 0x20,

        West = 0x40,

        NorthWest = 0x80,

        Horizontal = East | West,

        Vertical = North | South,

        Diagonal = NorthEast | SouthEast | SouthWest | NorthWest,

        MainAxes = North | East | South | West,

        All = North | NorthEast | East | SouthEast | South | SouthWest | West | NorthWest
    }
}
