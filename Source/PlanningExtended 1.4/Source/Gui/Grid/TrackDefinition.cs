namespace PlanningExtended.Gui.Grid
{
    internal class TrackDefinition
    {
        public TrackSizeType TrackSizeType { get; }

        public float Value { get; }

        public TrackDefinition(TrackSizeType trackSizeType, float value)
        {
            TrackSizeType = trackSizeType;
            Value = value;
        }

        //public static TrackDefinition Auto() => new(TrackSizeType.Auto, 0f);

        public static TrackDefinition Flexible(float value) => new(TrackSizeType.Flexible, value);

        public static TrackDefinition Fixed(float value) => new(TrackSizeType.Fixed, value);
    }
}
