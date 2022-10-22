using Verse;

namespace PlanningExtended.Cells
{
    public struct AreaDimensions
    {
        public int MinX { get; }

        public int MinZ { get; }

        public int MaxX { get; }

        public int MaxZ { get; }

        public int CenterX { get; }

        public int CenterZ { get; }

        public IntVec3 Min => new(MinX, 0, MinZ);

        public IntVec3 Max => new(MaxX, 0, MaxZ);

        public IntVec3 Center => new(CenterX, 0, CenterZ);

        public int Width => MaxX - MinX;

        public int Height => MaxZ - MinZ;

        public bool IsValid => MinX > -1000;

        public AreaDimensions(int minX, int minZ, int maxX, int maxZ)
        {
            MinX = minX;
            MinZ = minZ;
            MaxX = maxX;
            MaxZ = maxZ;
            CenterX = ((MaxX - MinX) / 2) + MinX;
            CenterZ = ((MaxZ - MinZ) / 2) + MinZ;
        }

        //public AreaDimensions MoveBy(IntVec3 offset)
        //{
        //    return new AreaDimensions(MinX + offset.x, MinZ + offset.z, MaxX + offset.x, MaxZ + offset.z);
        //}

        //public AreaDimensions Rotate()
        //{
        //    return new AreaDimensions(MinX, MinZ, MinX + (MaxZ - MinZ), MinZ + (MaxX - MinX));
        //}

        public static AreaDimensions operator +(AreaDimensions areaDimensions, IntVec3 intVec3)
        {
            return new AreaDimensions(areaDimensions.MinX + intVec3.x, areaDimensions.MinZ + intVec3.z, areaDimensions.MaxX + intVec3.x, areaDimensions.MaxZ + intVec3.z);
        }

        public override string ToString()
        {
            return $"({MinX}, {MinZ}) - ({MaxX}, {MaxZ})";
        }
    }
}
