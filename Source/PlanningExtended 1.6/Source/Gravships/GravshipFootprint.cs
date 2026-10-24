using Verse;

namespace PlanningExtended.Gravships;

/// <summary>
/// A substructure footprint source (grav engine, grav field extender, ...) of a gravship, relative to the grav engine at takeoff.
/// </summary>
public class GravshipFootprint : IExposable
{
    IntVec3 offset;
    public IntVec3 Offset => offset;

    float radius;
    public float Radius => radius;

    public GravshipFootprint()
    {
    }

    public GravshipFootprint(IntVec3 offset, float radius)
    {
        this.offset = offset;
        this.radius = radius;
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref offset, nameof(offset));
        Scribe_Values.Look(ref radius, nameof(radius));
    }
}
