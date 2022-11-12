using Verse;

namespace PlanningExtended.Plans
{
    public struct PlanCell : IExposable
    {
        IntVec2 position;
        public IntVec2 Position => position;

        PlanDesignitionType designation;
        public PlanDesignitionType Designation => designation;

        string color;
        public string Color => color;

        RotationDirection rotation;
        public RotationDirection Rotation => rotation;

        byte variant;
        public byte Variant => variant;

        public PlanCell()
            : this(IntVec2.Zero, PlanDesignitionType.Unknown, null)
        {
        }

        public PlanCell(IntVec2 position, PlanDesignitionType designation, string color)
            : this(position, designation, color, RotationDirection.None, 0)
        {
        
        }

        public PlanCell(IntVec2 position, PlanDesignitionType designation, string color, RotationDirection rotation, byte variant)
        {
            this.position = position;
            this.designation = designation;
            this.color = color;
            this.rotation = rotation;
            this.variant = variant;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref position, nameof(position));
            Scribe_Values.Look(ref designation, nameof(designation));
            Scribe_Values.Look(ref color, nameof(color));
            Scribe_Values.Look(ref rotation, nameof(rotation));
            Scribe_Values.Look(ref variant, nameof(variant));
        }

        public override string ToString()
        {
            return $"P({Position}), D({Designation}), C({Color})";
        }
    }
}
