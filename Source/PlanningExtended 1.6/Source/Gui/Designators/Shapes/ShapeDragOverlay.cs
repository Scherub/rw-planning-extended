using PlanningExtended.Cells;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Designators.Shapes;

/// <summary>
/// Draws the outline, side lengths and cell count of a dragged shape, like the vanilla <see cref="DesignationDragger"/> does for rectangles.
/// </summary>
[StaticConstructorOnStartup]
public static class ShapeDragOverlay
{
    // Same color as the vanilla drag outline.
    static readonly Texture2D OutlineTex = SolidColorMaterials.NewSolidColorTexture(new Color32(109, 139, 79, 100));

    public static void Draw(AreaDimensions area, IntVec3 cursor, int cellCount)
    {
        if (!area.IsValid)
            return;

        // The cursor is at one corner of the area, the drag started at the opposite one.
        IntVec3 end = new(GetNearest(cursor.x, area.MinX, area.MaxX), 0, GetNearest(cursor.z, area.MinZ, area.MaxZ));
        IntVec3 start = new(end.x == area.MinX ? area.MaxX : area.MinX, 0, end.z == area.MinZ ? area.MaxZ : area.MinZ);

        Vector2 startPosition = start.ToUIPosition();
        Vector2 endPosition = end.ToUIPosition();
        Vector2 centerPosition = (startPosition + endPosition) / 2f;

        if (area.Width > 1 || area.Height > 1)
        {
            Vector2 min = new Vector3(area.MinX, 0f, area.MinZ).MapToUIPosition();
            Vector2 max = new Vector3(area.MaxX + 1, 0f, area.MaxZ + 1).MapToUIPosition();

            Widgets.DrawBox(Rect.MinMaxRect(min.x, min.y, max.x, max.y), 1, OutlineTex);
        }

        if (area.Width >= 5)
            Widgets.DrawNumberOnMap(new Vector2(centerPosition.x, startPosition.y), area.Width, Color.white);

        if (area.Height >= 5)
            Widgets.DrawNumberOnMap(new Vector2(startPosition.x, centerPosition.y), area.Height, Color.white);

        if (cellCount > 0 && area.Width >= 3 && area.Height >= 3)
            Widgets.DrawNumberOnMap(centerPosition, cellCount, Color.white);
    }

    static int GetNearest(int value, int min, int max)
    {
        return Mathf.Abs(value - min) <= Mathf.Abs(value - max) ? min : max;
    }
}
