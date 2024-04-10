namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal abstract class Track
    {
        public int Index { get; }

        public TrackDefinition Definition { get; }

        public float Start { get; set; }

        public float Length { get; protected set; }

        public float End => Start + Length;

        public Track(int index, TrackDefinition definition)
        {
            Index = index;
            Definition = definition;
        }

        public abstract float ComputeLength(int numberOfTracks, float availableLength, float totalValue);

        public float ComputeStart(float start)
        {
            Start = start;

            return start + Length;
        }
    }
}
