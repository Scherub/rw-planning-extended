using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Controls.Grid.Tracks;
using UnityEngine;

namespace PlanningExtended.Gui.Controls
{
    internal class GridPanel : Panel
    {
        readonly List<TrackDefinition> _columnDefinitions;

        readonly List<TrackDefinition> _rowDefinitions;

        Dictionary<int, Track> _columns = new();

        Dictionary<int, Track> _rows = new();

        Rect _rect;

        public float ColumnGap { get; set; }

        public float RowGap { get; set; }

        public GridPanel(string columnDefinitions, string rowDefinitions, GridPosition? gridPosition = null, Thickness? margin = null, float columnGap = 0f, float rowGap = 0f)
            : base(gridPosition, margin)
        {
            _columnDefinitions = TrackDefinitionParser.Parse(columnDefinitions);
            _rowDefinitions = TrackDefinitionParser.Parse(rowDefinitions);
            ColumnGap = columnGap;
            RowGap = rowGap;
        }

        protected override void OnDraw(Rect rect)
        {
            Arrange(rect);

            foreach (BaseControl child in Children)
                child.Draw(GetRect(child.GridPosition));
        }

        //internal override Size Measure(Size availableSize)
        //{
        //    Size size = availableSize;

        //    foreach (BaseControl child in Children)
        //    {
        //        child.Measure(size);

        //    }
        //}

        protected override Size OnMeasure(Size availableSize)
        {
            
            
            foreach (BaseControl child in Children)
            {
                child.Measure(availableSize);

            }



            return base.OnMeasure(availableSize);
        }

        Rect GetRect(GridPosition gridPosition)
        {
            return GetRect(gridPosition.ColumnStartIndex, gridPosition.RowStartIndex, gridPosition.ColumnSpan, gridPosition.RowSpan);
        }

        Rect GetRect(int columnStartIndex, int rowStartIndex, int columnSpan = 1, int rowSpan = 1)
        {
            GetTrackStartEnd(ref _columns, columnStartIndex, columnSpan, out float startX, out float lengthX);
            GetTrackStartEnd(ref _rows, rowStartIndex, rowSpan, out float startY, out float lengthY);

            return new Rect(startX, startY, lengthX, lengthY);
        }

        void Arrange(Rect rect)
        {
            if (rect == _rect)
                return;

            _rect = rect;
            _columns = ComputeTracks(_columnDefinitions, rect.x, rect.width, Margin.Left, Margin.Right, ColumnGap);
            _rows = ComputeTracks(_rowDefinitions, rect.y, rect.height, Margin.Top, Margin.Bottom, RowGap);
        }

        Dictionary<int, Track> ComputeTracks(List<TrackDefinition> trackDefinitions, float start, float availableLength, float marginStart, float marginEnd, float gapLength)
        {
            float remainingLength = availableLength;

            // 1. define indices / order
            // 2. compute total gaps
            // 3. compute auto size
            // 3. compute fixed size
            // 4. compute flexible size
            // 5. compute start position

            List<Track> tracks = new();

            for (int i = 0; i < trackDefinitions.Count; i++)
                tracks.Add(CreateTrack(i, trackDefinitions[i]));

            float totalGapLength = (trackDefinitions.Count - 1) * gapLength;
            float totalMargin = marginStart + marginEnd;

            remainingLength -= totalGapLength;
            remainingLength -= totalMargin;

            remainingLength = ComputeTrackLength(tracks, TrackSizeType.Fixed, remainingLength);
            remainingLength = ComputeTrackLength(tracks, TrackSizeType.Flexible, remainingLength);

            ComputeStart(tracks, start + marginStart, gapLength);

            return tracks.ToDictionary(t => t.Index, t => t);
        }

        float ComputeTrackLength(List<Track> tracks, TrackSizeType trackSizeType, float availableLength)
        {
            return ComputeTrackLength(tracks.Where(td => td.Definition.TrackSizeType == trackSizeType).ToList(), availableLength);
        }

        float ComputeTrackLength(List<Track> tracks, float availableLength)
        {
            int numberOfTracks = tracks.Count;
            float totalTrackLength = tracks.Sum(t => t.Definition.Length);

            foreach (var track in tracks)
                availableLength = track.ComputeLength(numberOfTracks, availableLength, totalTrackLength);

            return availableLength;
        }

        void ComputeStart(List<Track> tracks, float start, float gapLength)
        {
            foreach (var track in tracks)
            {
                start = track.ComputeStart(start);
                start += gapLength;
            }
        }

        void GetTrackStartEnd(ref Dictionary<int, Track> tracks, int startIndex, int span, out float start, out float end)
        {
            if (!tracks.TryGetValue(startIndex, out Track trackStart))
                throw new GuiException($"Didn't find grid layout start track with startIndex of {startIndex}.");

            start = trackStart.Start;

            if (!tracks.TryGetValue(startIndex + span - 1, out Track trackEnd))
                throw new GuiException($"Didn't find grid layout end track with span of {startIndex + span - 1}.");

            end = trackEnd.Start + trackEnd.Length - trackStart.Start;
        }

        Track CreateTrack(int index, TrackDefinition trackDefinition)
        {
            return trackDefinition.TrackSizeType switch
            {
                TrackSizeType.Fixed => new FixedTrack(index, trackDefinition),
                TrackSizeType.Flexible => new FlexibleTrack(index, trackDefinition),
                _ => throw new GuiException("TrackDefinition of unknown type.")
            };
        }
    }
}
