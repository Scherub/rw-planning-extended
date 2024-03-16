using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Shapes.Plotter
{
    internal static class EllipsePlotter
    {
        public static IEnumerable<IntVec3> PlotEllipse(int centerX, int centerZ, int xDiameter, int zDiameter)
        {
            // based on McIlroy's ellipse raster algorithm: https://www.cs.dartmouth.edu/~doug/155.pdf
            // derivation by L. Patrick: http://enchantia.com/graphapp/doc/tech/ellipses.html
            // extended by Scherub to support even diameters

            bool hasEvenXDiameter = xDiameter % 2 == 0;
            bool hasEvenZDiameter = zDiameter % 2 == 0;

            int xRadius = xDiameter / 2 - (hasEvenXDiameter ? 1 : 0);
            int zRadius = zDiameter / 2 - (hasEvenZDiameter ? 1 : 0);

            int xRadiusSquare = xRadius * xRadius;
            int zRadiusSquare = zRadius * zRadius;

            var crit1 = -(xRadiusSquare / 4 + xRadius % 2 + zRadiusSquare);
            var crit2 = -(zRadiusSquare / 4 + zRadius % 2 + xRadiusSquare);
            var crit3 = -(zRadiusSquare / 4 + zRadius % 2);

            int x = 0;
            int z = zRadius;

            int t = xRadiusSquare * -z;
            int dxt = zRadiusSquare * x * 2;
            int dzt = xRadiusSquare * z * -2;
            int dx2t = zRadiusSquare * 2;
            int dz2t = xRadiusSquare * 2;

            while (z >= 0 && x <= xRadius)
            {
                yield return new IntVec3(centerX + x + (hasEvenXDiameter ? 1 : 0), 0, centerZ + z + (hasEvenZDiameter ? 1 : 0));
                yield return new IntVec3(centerX + x + (hasEvenXDiameter ? 1 : 0), 0, centerZ - z);
                yield return new IntVec3(centerX - x, 0, centerZ + z + (hasEvenZDiameter ? 1 : 0));
                yield return new IntVec3(centerX - x, 0, centerZ - z);

                if (t + x * zRadiusSquare <= crit1 || t + z * xRadiusSquare <= crit3)
                    (x, dxt, t) = IncrementX(x, dxt, dx2t, t);
                else if (t - z * xRadiusSquare > crit2)
                    (z, dzt, t) = IncrementZ(z, dzt, dz2t, t);
                else
                {
                    (x, dxt, t) = IncrementX(x, dxt, dx2t, t);
                    (z, dzt, t) = IncrementZ(z, dzt, dz2t, t);
                }
            }
        }

        static (int x, int dx, int t) IncrementX(int x, int dxt, int dx2t, int t)
        {
            return (++x, dxt + dx2t, t + dxt + dx2t);
        }

        static (int z, int dz, int t) IncrementZ(int z, int dzt, int dz2t, int t)
        {
            return (--z, dzt + dz2t, t + dzt + dz2t);
        }
    }
}
