namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal class FlexibleTrack : Track
    {
        public FlexibleTrack(int index, TrackDefinition definition)
            : base(index, definition)
        {
        }

        public override float ComputeLength(int numberOfTracks, float availableLength, float totalLength)
        {
            Length = (availableLength / totalLength) * Definition.Length;

            return availableLength;
        }
    }
}
