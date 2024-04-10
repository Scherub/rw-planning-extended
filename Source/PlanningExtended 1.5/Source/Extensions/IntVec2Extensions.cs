using System;
using Verse;

namespace PlanningExtended
{
    public static class IntVec2Extensions
    {
        public static IntVec2 Flip(this IntVec2 position, FlipDirection flipDirection, IntVec2 areaSize)
        {
            if (flipDirection == FlipDirection.Horizontally)
                return new(areaSize.x - position.x, position.z);
            else
                return new(position.x, areaSize.z - position.z);
        }

        public static IntVec2 Rotate(this IntVec2 position, RotationDirection rotationDirection)
        {
            return rotationDirection switch
            {
                RotationDirection.None => position,
                RotationDirection.Clockwise => new IntVec2(position.z, -position.x),
                RotationDirection.Opposite => new IntVec2(-position.x, -position.z),
                RotationDirection.Counterclockwise => new IntVec2(-position.z, position.x),
                _ => throw new NotImplementedException(),
            };
        }

        public static IntVec2 Rotate(this IntVec2 position, RotationDirection rotationDirection, IntVec2 offset)
        {
            return rotationDirection switch
            {
                RotationDirection.None => position,
                RotationDirection.Clockwise => new IntVec2(position.z, -position.x) + offset,
                RotationDirection.Opposite => new IntVec2(-position.x, -position.z) + offset,
                RotationDirection.Counterclockwise => new IntVec2(-position.z, position.x) + offset,
                _ => throw new NotImplementedException(),
            };
        }

        public static IntVec2 Inverse(this IntVec2 position)
        {
            return new IntVec2(-position.x, -position.z);
        }

        public static IntVec2 DetermineOffsetForRotation(this IntVec2 position)
        {
            return new IntVec2(-position.x < 0 ? 0 : -position.x, -position.z < 0 ? 0 : -position.z);
        }
    }
}
