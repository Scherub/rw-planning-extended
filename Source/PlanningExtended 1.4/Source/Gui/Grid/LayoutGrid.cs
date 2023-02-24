using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Gui.Grid.Tracks;
using UnityEngine;

namespace PlanningExtended.Gui.Grid
{
    internal class LayoutGrid
    {
        readonly List<TrackDefinition> _columnDefinitions;

        readonly List<TrackDefinition> _rowDefinitions;

        readonly Thickness _margin;

        readonly float _columnGap;

        readonly float _rowGap;

        Rect _rect;

        Dictionary<int, Track> _columns = new();

        Dictionary<int, Track> _rows = new();

        public LayoutGrid(IEnumerable<TrackDefinition> columnDefinitions, IEnumerable<TrackDefinition> rowDefinitions, float columnGap = 0f, float rowGap = 0f)
            : this(columnDefinitions, rowDefinitions, Thickness.Zero, columnGap, rowGap)
        {
        }

        public LayoutGrid(IEnumerable<TrackDefinition> columnDefinitions, IEnumerable<TrackDefinition> rowDefinitions, Thickness margin, float columnGap = 0f, float rowGap = 0f)
        {
            _columnDefinitions = columnDefinitions.ToList();
            _rowDefinitions = rowDefinitions.ToList();
            _margin = margin;
            _columnGap = columnGap;
            _rowGap = rowGap;
        }

        public void Compute(Rect rect)
        {
            if (rect == _rect)
                return;

            _rect = rect;
            _columns = ComputeTracks(_columnDefinitions, rect.x, rect.width, _margin.Left, _margin.Right, _columnGap);
            _rows = ComputeTracks(_rowDefinitions, rect.y, rect.height, _margin.Top, _margin.Bottom, _rowGap);
        }

        public Rect GetRect(int columnStartIndex, int rowStartIndex, int columnSpan = 1, int rowSpan = 1)
        {
            GetTrackStartEnd(ref _columns, columnStartIndex, columnSpan, out float startX, out float lengthX);
            GetTrackStartEnd(ref _rows, rowStartIndex, rowSpan, out float startY, out float lengthY);

            return new Rect(startX, startY, lengthX, lengthY);
        }

        Dictionary<int, Track> ComputeTracks(List<TrackDefinition> trackDefinitions, float start, float availableLength, float marginStart, float marginEnd, float gapLength)
        {
            float remainingLength = availableLength;

            // 1. define indices / order
            // 2. compute total gaps
            // 3. compute fixed size
            // 4. compute flexibel size
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
            float totalValue = tracks.Sum(t => t.Definition.Value);

            foreach (var track in tracks)
                availableLength = track.ComputeLength(numberOfTracks, availableLength, totalValue);

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
                throw new System.Exception();

            start = trackStart.Start;

            if (!tracks.TryGetValue(startIndex + span - 1, out Track trackEnd))
                throw new System.Exception();

            end = trackEnd.Start + trackEnd.Length - trackStart.Start;
        }

        Track CreateTrack(int index, TrackDefinition trackDefinition)
        {
            return trackDefinition.TrackSizeType switch
            {
                TrackSizeType.Fixed => new FixedTrack(index, trackDefinition),
                TrackSizeType.Flexible => new FlexibleTrack(index, trackDefinition),
                _ => throw new System.Exception()
            };
        }
    }
}
