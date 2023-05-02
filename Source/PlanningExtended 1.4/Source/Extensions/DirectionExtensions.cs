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

        public static Direction GetNextClockwise(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.NorthEast,
                Direction.NorthEast => Direction.East,
                Direction.East => Direction.SouthEast,
                Direction.SouthEast => Direction.South,
                Direction.South => Direction.SouthWest,
                Direction.SouthWest => Direction.West,
                Direction.West => Direction.NorthWest,
                Direction.NorthWest => Direction.North,
                Direction.Horizontal => Direction.Vertical,
                Direction.Vertical => Direction.Horizontal,
                _ => Direction.None
            };
        }

        public static Direction GetNextClockwise(this Direction direction, Direction availableDirections)
        {
            do
            {
                direction = direction.GetNextClockwise();
            } while ((direction & availableDirections) == 0);

            return direction;
        }

        public static Direction GetNextCounterClockwise(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.NorthWest,
                Direction.NorthWest => Direction.West,
                Direction.West => Direction.SouthWest,
                Direction.SouthWest => Direction.South,
                Direction.South => Direction.SouthEast,
                Direction.SouthEast => Direction.East,
                Direction.East => Direction.NorthEast,
                Direction.NorthEast => Direction.North,
                Direction.Horizontal => Direction.Vertical,
                Direction.Vertical => Direction.Horizontal,
                _ => Direction.None
            };
        }

        public static Direction GetNextCounterClockwise(this Direction direction, Direction availableDirections)
        {
            do
            {
                direction = direction.GetNextCounterClockwise();
            } while ((direction & availableDirections) == 0);

            return direction;
        }
    }
}
