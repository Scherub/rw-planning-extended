using Verse;

namespace PlanningExtended.Plans;

public struct PlanCell : IExposable
{
    IntVec2 position;
    public readonly IntVec2 Position => position;

    PlanDesignationType designation;
    public readonly PlanDesignationType Designation => designation;

    string color;
    public readonly string Color => color;

    RotationDirection rotation;
    public readonly RotationDirection Rotation => rotation;

    byte variant;
    public readonly byte Variant => variant;

    public PlanCell()
        : this(IntVec2.Zero, PlanDesignationType.Unknown, null)
    {
    }

    public PlanCell(IntVec2 position, PlanDesignationType designation, string color)
        : this(position, designation, color, RotationDirection.None, 0)
    {
    
    }

    public PlanCell(IntVec2 position, PlanDesignationType designation, string color, RotationDirection rotation, byte variant)
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

    public override readonly string ToString()
    {
        return $"P({Position}), D({Designation}), C({Color})";
    }
}
