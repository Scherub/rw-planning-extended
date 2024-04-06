namespace PlanningExtended.Gui.Controls.Grid
{
    internal class TrackDefinition
    {
        public TrackSizeType TrackSizeType { get; }

        public float Length { get; }

        public TrackDefinition(TrackSizeType trackSizeType, float length)
        {
            TrackSizeType = trackSizeType;
            Length = length;
        }

        public static TrackDefinition Auto() => new(TrackSizeType.Auto, 0f);

        public static TrackDefinition Flexible(float value) => new(TrackSizeType.Flexible, value);

        public static TrackDefinition Fixed(float value) => new(TrackSizeType.Fixed, value);
    }
}
