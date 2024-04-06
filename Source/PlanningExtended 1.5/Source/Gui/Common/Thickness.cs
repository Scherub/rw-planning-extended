namespace PlanningExtended.Gui
{
    internal readonly struct Thickness
    {
        public static Thickness Zero = new();

        public float Left { get; }

        public float Top { get; }

        public float Right { get; }

        public float Bottom { get; }

        public float Width => Left + Right;

        public float Height => Top + Bottom;

        public Thickness()
            : this(0f, 0f, 0f, 0f)
        {
        }

        public Thickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static Thickness All(float value) => new(value, value, value, value);

        public static Thickness Symmetric(float horizontal, float vertical) => new(horizontal, vertical, horizontal, vertical);

        public static Thickness Only(float left = 0f, float top = 0f, float right = 0f, float bottom = 0f) => new(left, top, right, bottom);
    }
}
