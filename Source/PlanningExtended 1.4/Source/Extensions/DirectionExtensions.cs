namespace PlanningExtended
{
    internal static class DirectionExtensions
    {
        public static Direction GetOpposite(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.NorthEast => Direction.SouthWest,
                Direction.East => Direction.West,
                Direction.SouthEast => Direction.NorthWest,
                Direction.South => Direction.North,
                Direction.SouthWest => Direction.NorthEast,
                Direction.West => Direction.East,
                Direction.NorthWest => Direction.SouthEast,
                _ => Direction.None
            };
        }
    }
}
