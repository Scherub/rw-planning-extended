using UnityEngine;
using Verse;

namespace PlanningExtended.Plans
{
    public struct PlanCell
    {
        public IntVec2 Position { get; }

        public PlanDesignitionType Designation { get; }

        public string Color { get; }
        
        public RotationDirection Rotation { get; }

        public byte Variant { get; }

        public PlanCell(IntVec2 position, PlanDesignitionType designation, string color)
            : this(position, designation, color, RotationDirection.None, 0)
        {
        
        }

        public PlanCell(IntVec2 position, PlanDesignitionType designation, string color, RotationDirection rotation, byte variant)
        {
            Position = position;
            Designation = designation;
            Color = color;
            Rotation = rotation;
            Variant = variant;
        }

        public override string ToString()
        {
            return $"P({Position}), D({Designation}), C({Color})";
        }
    }
}
