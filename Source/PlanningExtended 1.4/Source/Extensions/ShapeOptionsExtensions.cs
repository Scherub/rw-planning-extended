namespace PlanningExtended
{
    internal static class ShapeOptionsExtensions
    {
        public static ShapeDisplayOptions GetShapeDisplayOptions(this ShapeOptions options)
        {
            return options switch
            {
                ShapeOptions.NumberOfSegmentsX or ShapeOptions.NumberOfSegmentsZ => ShapeDisplayOptions.NumberOfSegments,
                ShapeOptions.Rotation=> ShapeDisplayOptions.Rotation,
                ShapeOptions.Thickness => ShapeDisplayOptions.Thickness,
                _ => ShapeDisplayOptions.None,
            };
        }
    }
}
