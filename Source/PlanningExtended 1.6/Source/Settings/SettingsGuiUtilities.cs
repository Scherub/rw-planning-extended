using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Plans.Converters;
using PlanningExtended.Updates;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Settings
{
    public static class SettingsGuiUtilities
    {
        static readonly List<StartupPlanVisibility> startupPlanVisibilityValues = Enum.GetValues(typeof(StartupPlanVisibility)).Cast<StartupPlanVisibility>().ToList();

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

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayChangePlanAppearanceDesignator.Label".Translate(), ref settings.displayChangePlanAppearanceDesignator, "PlanningExtended.Settings.DisplayChangePlanAppearanceDesignator.Desc".Translate());

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Label".Translate(), ref settings.displayTogglePlanVisibilityDesignator, "PlanningExtended.Settings.DisplayTogglePlanVisibilityDesignator.Desc".Translate());

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseCtrlForColorDialog.Label".Translate(), ref settings.useCtrlForColorDialog, "PlanningExtended.Settings.UseCtrlForColorDialog.Desc".Translate());

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.ArePlansPersistent.Label".Translate(), ref settings.areDesignationsPersistent, "PlanningExtended.Settings.ArePlansPersistent.Desc".Translate());

            listingStandard.CheckboxLabeled("PlanningExtended.Settings.UseSkipInsteadOfReplaceAsDefault.Label".Translate(), ref settings.useSkipInsteadOfReplaceAsDefault, "PlanningExtended.Settings.UseSkipInsteadOfReplaceAsDefault.Desc".Translate());

            if (listingStandard.ButtonTextLabeled("PlanningExtended.Settings.StartupPlanVisibility.Label".Translate(), settings.startupPlanVisibility.Translate(), tooltip: "PlanningExtended.Settings.StartupPlanVisibility.Desc".Translate()))
            {
                var startupPlanVisibilityOptions = startupPlanVisibilityValues.Select(v => new FloatMenuOption(v.Translate(), () => PlanningMod.Settings.SetStartupPlanVisibility(v))).ToList();

                Find.WindowStack.Add(new FloatMenu(startupPlanVisibilityOptions));
            }

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

            listingStandard.Gap(24f);

            //listingStandard.BeginSubSection();

            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {

                if (listingStandard.ButtonTextLabeledPct((string)"PlanningExtended.Settings.PlanningDataFolder.Label".Translate(), (string)"OpenFolder".Translate(), 0.6f, TextAnchor.MiddleLeft))
                {
                    Application.OpenURL(PlanFilePaths.PlanFolderPath);
                    SoundDefOf.Click.PlayOneShotOnCamera();
                }

                listingStandard.SubLabel(PlanFilePaths.PlanFolderPath, 0.6f);
            }

            //listingStandard.EndSubSection();

            //listingStandard.Heading("ResetButton".Translate());

            listingStandard.End();
        }
    }
}
