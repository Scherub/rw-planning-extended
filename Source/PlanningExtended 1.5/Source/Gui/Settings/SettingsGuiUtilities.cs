using PlanningExtended.Plans.Converters;
using PlanningExtended.Settings;
using PlanningExtended.Updates;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Settings
{
    public static class SettingsGuiUtilities
    {
        public static void DisplaySettings(PlanningSettings settings, Rect inRect)
        {
            float margin = 16f;
            float columnWidth = inRect.width / 2f;
            float usableColumnWidth = columnWidth - margin * 2f;

            DisplayLeftPane(settings, new Rect(inRect.x + margin, inRect.y, usableColumnWidth, inRect.height));

            DisplayRightPane(settings, new Rect(inRect.x + columnWidth + margin, inRect.y, usableColumnWidth, inRect.height));
        }

        static void DisplayLeftPane(PlanningSettings settings, Rect inRect)
        {
            Listing_Standard listingStandard = new();

            listingStandard.Begin(inRect);

            //listingStandard.Heading("PlanningExtended.Settings.UndoRedo.Label".Translate());

            //listingStandard.BeginSubSection();


            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseUndoRedo.Label".Translate(), ref settings.General.useUndoRedo, "PlanningExtended.Settings.UseUndoRedo.Desc".Translate());

            settings.General.maxUndoOperations = listingStandard.SliderLabel(settings.General.maxUndoOperations, 5, 50, () => "PlanningExtended.Settings.MaxUndoRedoOperations.Label".Translate() + " (5 - 50): " + settings.General.maxUndoOperations);

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayCutDesignator.Label".Translate(), ref settings.General.displayCutDesignator, "PlanningExtended.Settings.DisplayCutDesignator.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayChangePlanAppearanceDesignator.Label".Translate(), ref settings.General.displayChangePlanAppearanceDesignator, "PlanningExtended.Settings.DisplayChangePlanAppearanceDesignator.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Label".Translate(), ref settings.General.displayTogglePlanVisibilityDesignator, "PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseCtrlForColorDialog.Label".Translate(), ref settings.General.useCtrlForColorDialog, "PlanningExtended.Settings.UseCtrlForColorDialog.Desc".Translate());
            
            listingStandard.CheckboxLabeled("PlanningExtended.Settings.ArePlansPersistent.Label".Translate(), ref settings.General.areDesignationsPersistent, "PlanningExtended.Settings.ArePlansPersistent.Desc".Translate());

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseSkipInsteadOfReplaceAsDefault.Label".Translate(), ref settings.General.useSkipInsteadOfReplaceAsDefault, "PlanningExtended.Settings.UseSkipInsteadOfReplaceAsDefault.Desc".Translate());

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

            if (listingStandard.ButtonTextLabeled("PlanningExtended.Settings.UpgradeOldPlans.Label".Translate(), "PlanningExtended.Actions.Upgrade".Translate()))
                PlanUpdateManager.ApplyUpdates();

            if (listingStandard.ButtonTextLabeled("PlanningExtended.Settings.ConvertVanillaPlans.Label".Translate(), "PlanningExtended.Actions.Convert".Translate()))
                VanillaPlanConverter.Convert();

            if (listingStandard.ButtonTextLabeled("PlanningExtended.Settings.ConvertMorePlanningPlans.Label".Translate(), "PlanningExtended.Actions.Convert".Translate()))
                MorePlanningConverter.Convert();

            //listingStandard.Heading("SCE_WorkingAndLearningSpeed_Label".Translate(), "SCE_WorkingAndLearningSpeed_Description".Translate());

            //listingStandard.BeginSubSection();

            //listingStandard.EndSubSection();

            listingStandard.End();
        }
    }
}
