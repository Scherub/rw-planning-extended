using Verse;

namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal class FixedTrack : Track
    {
        public FixedTrack(int index, TrackDefinition definition)
            : base(index, definition)
        {
        }

        public override float ComputeLength(int numberOfTracks, float availableLength, float totalLength)
        {
            if (Definition.Length < 0)
                throw new GuiException($"FixedTrack.Length {Definition.Length} must not be < 0.");
            else if (Definition.Length > availableLength)
            {
                Length = availableLength;
                Log.Error($"FixedTrack.Length {Definition.Length} must not be > available length {availableLength}.");
                //throw new GuiException($"FixedTrack.Length {Definition.Value} must not be > available length {availableLength}.");
            }
            else
                Length = Definition.Length;

            return availableLength - Length;
        }
    }
}
