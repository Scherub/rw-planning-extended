using UnityEngine;
using Verse;

namespace PlanningExtended.Settings
{
    public class ToolboxSettings : BaseSettings
    {
        Vector2 windowPosition;

        public ToolboxSettings(PlanningSettings planningSettings)
            : base(planningSettings)
        {
            
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref windowPosition, nameof(windowPosition), Vector2.zero);
        }

        public void SetWindowPosition(Vector2 position, bool autoSave = true)
        {
            windowPosition = position;

            if (autoSave)
                PlanningSettings.Write();
        }

        public Vector2 GetWindowPosition()
        {
            return windowPosition;
        }
    }
}
