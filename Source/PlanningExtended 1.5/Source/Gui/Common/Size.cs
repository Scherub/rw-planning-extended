namespace PlanningExtended.Gui
{
    internal readonly struct Size
    {
        public static readonly Size Zero = new();

        public static readonly Size Infinite = new(float.PositiveInfinity, float.PositiveInfinity);

        public float Width { get; }

        public float Height { get; }

        public bool IsZero => Width == 0f && Height == 0f;

        public Size()
            : this(0f, 0f)
        {
        }

        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
