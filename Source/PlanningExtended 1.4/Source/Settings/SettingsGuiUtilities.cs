using PlanningExtended.Updates;
using UnityEngine;
using Verse;

namespace PlanningExtended.Settings
{
    public static class SettingsGuiUtilities
    {
        public static void DisplaySettings(PlanningSettings settings, Rect inRect)
        {
            float margin = 16f;
            float columnWidth = inRect.width / 2f;
            float usableColumnWidth = columnWidth - margin * 2f;

            DisplaySkillSelection(settings, new Rect(inRect.x + margin, inRect.y, usableColumnWidth, inRect.height));

            DisplayRightPane(settings, new Rect(inRect.x + columnWidth + margin, inRect.y, usableColumnWidth, inRect.height));
        }

        static void DisplaySkillSelection(PlanningSettings settings, Rect inRect)
        {
            Listing_Standard listingStandard = new();

            listingStandard.Begin(inRect);

            //listingStandard.Heading("PlanningExtended.Settings.UndoRedo.Label".Translate());

            //listingStandard.BeginSubSection();


            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseUndoRedo.Label".Translate(), ref settings.useUndoRedo, "PlanningExtended.Settings.UseUndoRedo.Desc".Translate());

            settings.maxUndoOperations = listingStandard.SliderLabel(settings.maxUndoOperations, 5, 50, () => "PlanningExtended.Settings.MaxUndoRedoOperations.Label".Translate() + " (5 - 50): " + settings.maxUndoOperations);

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayCutDesignator.Label".Translate(), ref settings.displayCutDesignator, "PlanningExtended.Settings.DisplayCutDesignator.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Label".Translate(), ref settings.displayTogglePlanVisibilityDesignator, "PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.ArePlansPersistent.Label".Translate(), ref settings.areDesignationsPersistent, "PlanningExtended.Settings.ArePlansPersistent.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseCtrlForColorDialog.Label".Translate(), ref settings.useCtrlForColorDialog, "PlanningExtended.Settings.UseCtrlForColorDialog.Desc".Translate());
            
            //listingStandard.EndSubSection();

            listingStandard.Gap(24f);

            listingStandard.Heading("ResetButton".Translate());

            listingStandard.BeginSubSection();

            if (listingStandard.ButtonTextLabeled("RestoreToDefaultSettings".Translate(), "ResetButton".Translate()))
                settings.Reset();

            listingStandard.EndSubSection();

            listingStandard.End();
        }

        static void DisplayRightPane(PlanningSettings settings, Rect inRect)
        {
            Listing_Standard listingStandard = new();

            listingStandard.Begin(inRect);

            if (listingStandard.ButtonTextLabeled("Upgrade old plan designations", "Upgrade"))
                UpdatePlanDesignations.Update();

            //listingStandard.Heading("SCE_WorkingAndLearningSpeed_Label".Translate(), "SCE_WorkingAndLearningSpeed_Description".Translate());

            //listingStandard.BeginSubSection();

            //listingStandard.EndSubSection();

            listingStandard.End();
        }
    }
}
