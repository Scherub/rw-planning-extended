namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal class FlexibleTrack : Track
    {
        public FlexibleTrack(int index, TrackDefinition definition)
            : base(index, definition)
        {
        }

        public override float ComputeLength(int numberOfTracks, float availableLength, float totalValue)
        {
            Length = (availableLength / totalValue) * Definition.Value;

            return availableLength;
        }
    }
}
