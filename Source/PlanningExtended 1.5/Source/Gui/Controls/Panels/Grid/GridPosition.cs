namespace PlanningExtended.Gui.Controls.Grid
{
    internal readonly struct GridPosition
    {
        public static readonly GridPosition Zero = new();

        public int ColumnStartIndex { get; }
        
        public int RowStartIndex { get; }

        public int ColumnSpan { get; }

        public int RowSpan { get; }

        public GridPosition()
            : this(0, 0)
        {
        }

        public GridPosition(int columnStartIndex, int rowStartIndex)
            : this(columnStartIndex, rowStartIndex, 1, 1)
        {
        }

        public GridPosition(int columnStartIndex, int rowStartIndex, int columnSpan, int rowSpan)
        {
            ColumnStartIndex = columnStartIndex;
            RowStartIndex = rowStartIndex;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }

        public static GridPosition StartIndex(int columnStartIndex, int rowStartIndex)
        {
            return new GridPosition(columnStartIndex, rowStartIndex);
        }
    }
}
