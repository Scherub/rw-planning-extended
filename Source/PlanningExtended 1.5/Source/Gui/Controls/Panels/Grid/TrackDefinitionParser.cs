using System;
using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Gui.Controls.Grid
{
    internal static class TrackDefinitionParser
    {
        public static List<TrackDefinition> Parse(string tracksText)
        {
            List<TrackDefinition> trackDefinitions = new();

            string[] trackTextArray = tracksText.Split(',');

            try
            {
                foreach (string trackText in trackTextArray)
                {
                    string trimmedtrackText = trackText.Trim();

                    if (trimmedtrackText.Contains("auto"))
                    {
                        trackDefinitions.Add(TrackDefinition.Auto());
                    }
                    else if (trimmedtrackText.EndsWith("*"))
                    {
                        float length = trimmedtrackText.Length == 1 ? 1f : float.Parse(trimmedtrackText.Substring(0, trimmedtrackText.Length - 1));
                        trackDefinitions.Add(TrackDefinition.Flexible(length));
                    }
                    else if (trimmedtrackText.EndsWith("px"))
                    {
                        float length = float.Parse(trimmedtrackText.Substring(0, trimmedtrackText.Length - 2));
                        trackDefinitions.Add(TrackDefinition.Fixed(length));
                    }
                    else
                    {
                        float length = float.Parse(trimmedtrackText);
                        trackDefinitions.Add(TrackDefinition.Fixed(length));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Could not parse track definition: {tracksText}\n{ex.Message}");
            }
            
            return trackDefinitions;
        }
    }
}
