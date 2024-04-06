using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Designations;
using PlanningExtended.Plans.Appearances;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Utilities
{
    internal static class GuiMenuOptionsUtilities
    {
        static readonly List<int> OpacityList = Enumerable.Range(1, 100).Where(i => i % 10 == 0).ToList();

        static readonly List<PlanTextureSet> PlanTextureSets = Enum.GetValues(typeof(PlanTextureSet)).Cast<PlanTextureSet>().ToList();

        public static List<FloatMenuOption> GetPlanTypeMenuOptions(Action<PlanDesignationType> action)
        {
            List<FloatMenuOption> list = [];

            foreach (PlanDesignationType planDesignationType in PlanDesignationUtilities.GetPlanDesignationTypes())
                list.Add(new FloatMenuOption(planDesignationType.ToString(), () => action(planDesignationType)));

            return list;
        }

        public static List<FloatMenuOption> GetOpacityMenuOptions(PlanDesignationType planDesignationType)
        {
            List<FloatMenuOption> list = [];

            foreach (int opacity in OpacityList)
                list.Add(new FloatMenuOption($"{opacity}%", () => PlanAppearanceManager.SetPlanOpacity(planDesignationType, opacity)));

            return list;
        }

        public static List<FloatMenuOption> GetTextureSetsMenuOptions(PlanDesignationType planDesignationType)
        {
            List<FloatMenuOption> list = [];

            foreach (PlanTextureSet planTextureSet in PlanTextureSets)
                list.Add(new FloatMenuOption(planTextureSet.ToString(), () => PlanAppearanceManager.SetPlanTextureSet(planDesignationType, planTextureSet)));

            return list;
        }

        public static List<FloatMenuGridOption> GetColorSelectionMenuGridOptions(Designator colorPicker, Action<ColorDef> onColorSelection)
        {
            List<FloatMenuGridOption> list = new(ColorDefinitions.ColorDefs.Count + 1)
            {
                new FloatMenuGridOption(Designator_Eyedropper.EyeDropperTex, () =>
                {
                    Find.DesignatorManager.Select(colorPicker);
                }, null, new TipSignal?("DesignatorEyeDropperDesc_Paint".Translate()))
            };

            foreach (ColorDef colorDef in ColorDefinitions.ColorDefs)
            {
                list.Add(new FloatMenuGridOption(BaseContent.WhiteTex, () =>
                {
                    onColorSelection?.Invoke(colorDef);
                }, new Color?(colorDef.color), new TipSignal?(colorDef.LabelCap)));
            }

            return list;
        }
    }
}
