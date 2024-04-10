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

        DiagonalNW = 0x100,

        DiagonalNE = 0x200,

        Horizontal = East | West,

        Vertical = North | South,

        Diagonal = NorthEast | SouthEast | SouthWest | NorthWest,

        DiagonalAll = Diagonal | DiagonalNE | DiagonalNW,

        MainAxes = North | East | South | West,

        All = North | NorthEast | East | SouthEast | South | SouthWest | West | NorthWest
    }
}
