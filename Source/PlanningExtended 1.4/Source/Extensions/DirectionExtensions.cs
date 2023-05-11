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
                Direction.Vertical => Direction.Diagonal,
                Direction.Diagonal => Direction.DiagonalSWNE,
                Direction.DiagonalSWNE => Direction.DiagonalSENW,
                Direction.DiagonalSENW => Direction.Horizontal,
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
                Direction.Horizontal => Direction.DiagonalSENW,
                Direction.DiagonalSENW => Direction.DiagonalSWNE,
                Direction.DiagonalSWNE => Direction.Diagonal,
                Direction.Diagonal => Direction.Vertical,
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

        public static float GetAngle(this Direction direction)
        {
            return direction switch
            {
                Direction.North => 0f,
                Direction.NorthEast => 45f,
                Direction.East => 90f,
                Direction.SouthEast => 135f,
                Direction.South => 180f,
                Direction.SouthWest => 225f,
                Direction.West => 270f,
                Direction.NorthWest => 315f,
                _ => 0f
            };
        }

        public static int GetMultiplier(this Direction direction, Direction expectedDirection)
        {
            return direction switch
            {
                Direction.North => expectedDirection == Direction.North ? 1 : -1,
                Direction.NorthEast => expectedDirection == Direction.NorthEast ? 1 : -1,
                Direction.East => expectedDirection == Direction.East ? 1 : -1,
                Direction.SouthEast => expectedDirection == Direction.SouthEast ? 1 : -1,
                Direction.South => expectedDirection == Direction.South ? 1 : -1,
                Direction.SouthWest => expectedDirection == Direction.SouthWest ? 1 : -1,
                Direction.West => expectedDirection == Direction.West ? 1 : -1,
                Direction.NorthWest => expectedDirection == Direction.NorthWest ? 1 : -1,
                _ => 0
            };
        }
    }
}
