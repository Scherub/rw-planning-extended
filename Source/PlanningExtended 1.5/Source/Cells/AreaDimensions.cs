using Verse;

namespace PlanningExtended.Cells
{
    // TODO: does it need to be a record
    public readonly record struct AreaDimensions
    {
        public int MinX { get; }

        public int MinZ { get; }

        public int MaxX { get; }

        public int MaxZ { get; }

        public int CenterX { get; }

        public int CenterZ { get; }

        public float RealCenterX { get; }
        
        public float RealCenterZ { get; }

        public IntVec3 Min => new(MinX, 0, MinZ);

        public IntVec3 Max => new(MaxX, 0, MaxZ);

        public IntVec3 Center => new(CenterX, 0, CenterZ);

        public int Width => MaxX - MinX + 1;

        public int Height => MaxZ - MinZ + 1;

        public bool IsValid => MinX > -1000;

        public AreaDimensions(int minX, int minZ, int maxX, int maxZ)
        {
            MinX = minX;
            MinZ = minZ;
            MaxX = maxX;
            MaxZ = maxZ;
            CenterX = ((MaxX - MinX) / 2) + MinX;
            CenterZ = ((MaxZ - MinZ) / 2) + MinZ;
            RealCenterX = ((MaxX - MinX) / 2f) + MinX;
            RealCenterZ = ((MaxZ - MinZ) / 2f) + MinZ;
        }

        public AreaDimensions Merge(AreaDimensions areaDimensions)
        {
            if (IsValid && areaDimensions.IsValid)
            {
                return new AreaDimensions(
                    MinX < areaDimensions.MinX ? MinX : areaDimensions.MinX,
                    MinZ < areaDimensions.MinZ ? MinZ : areaDimensions.MinZ,
                    MaxX > areaDimensions.MaxX ? MaxX : areaDimensions.MaxX,
                    MaxZ > areaDimensions.MaxZ ? MaxZ : areaDimensions.MaxZ
                    );
            }
            else if (IsValid)
                return this;

            return areaDimensions;
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
