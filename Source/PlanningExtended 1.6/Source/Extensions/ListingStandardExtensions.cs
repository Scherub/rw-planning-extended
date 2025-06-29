using System;
using Verse;

namespace PlanningExtended
{
    public static class ListingStandardExtensions
    {
        public static void BeginSubSection(this Listing_Standard listingStandard, float gapWidth = 12f)
        {
            listingStandard.Indent(gapWidth);
            listingStandard.ColumnWidth -= gapWidth;
        }

        public static void EndSubSection(this Listing_Standard listingStandard, float gapWidth = 12f)
        {
            listingStandard.Outdent(gapWidth);
            listingStandard.ColumnWidth += gapWidth;
        }

        public static void Heading(this Listing_Standard listingStandard, string label, string tooltip = null)
        {
            GameFont oldFont = Text.Font;
            Text.Font = GameFont.Medium;

            listingStandard.Label(new(label), -1f, tooltip);
            listingStandard.Gap();

            Text.Font = oldFont;
        }

        public static bool RadioButtonLabel(this Listing_Standard listingStandard, string label, bool isActive)
        {
            bool result = listingStandard.RadioButton(label, isActive);

            listingStandard.Gap(4f);

            return result;
        }

        public static int SliderLabel(this Listing_Standard listingStandard, int value, int min, int max, Func<string> label)
        {
            return (int)SliderLabel(listingStandard, (float)value, (float)min, (float)max, label);
        }

        public static float SliderLabel(this Listing_Standard listingStandard, float value, float min, float max, Func<string> label)
        {
            GameFont oldFont = Text.Font;
            //Text.Font = GameFont.Tiny;

            if (label != null)
                listingStandard.Label(label());

            value = listingStandard.Slider(value, min, max);
            value = (float)Math.Round(value, 2);

            Text.Font = oldFont;

            return value;
        }
    }
}
