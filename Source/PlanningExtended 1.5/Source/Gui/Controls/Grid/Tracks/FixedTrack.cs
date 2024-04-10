namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal class FixedTrack : Track
    {
        public FixedTrack(int index, TrackDefinition definition)
            : base(index, definition)
        {
        }

        public override float ComputeLength(int numberOfTracks, float availableLength, float totalValue)
        {
            if (Definition.Value < 0)
                throw new System.Exception();
            else if (Definition.Value > availableLength)
                throw new System.Exception();

            Length = Definition.Value;

            return availableLength - Length;
        }
    }
}
