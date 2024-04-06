using System;

namespace PlanningExtended.Gui.Controls.Grid.Tracks
{
    internal class AutoTrack : Track
    {
        public AutoTrack(int index, TrackDefinition definition)
            : base(index, definition)
        {
        }

        public override float ComputeLength(int numberOfTracks, float availableLength, float totalLength)
        {
            throw new NotImplementedException();
        }
    }
}
