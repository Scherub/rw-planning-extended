using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Materials;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class ChangePlanAppearanceDesignator : BasePlanMenuDesignator
    {
        List<int> OpacityList => Enumerable.Range(1, 100).Where(i => i % 10 == 0).ToList();

        List<PlanTextureSet> PlanTextureSets => Enum.GetValues(typeof(PlanTextureSet)).Cast<PlanTextureSet>().ToList();

        public override bool Visible => PlanningMod.Settings.displayChangePlanAppearanceDesignator;

        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetMenuOptions((planDesignationType) => GetTextureSetMenuOptions(planDesignationType));

        public ChangePlanAppearanceDesignator()
            : base("ChangePlanAppearance")
        {
        }

        public override void ProcessInput(Event ev)
        {
            List<FloatMenuOption> list = GetMenuOptions((planDesignationType) => GetOpacityMenuOptions(planDesignationType));

            Find.WindowStack.Add(new FloatMenu(list));
        }

        List<FloatMenuOption> GetOpacityMenuOptions(PlanDesignationType planDesignationType)
        {
            List<FloatMenuOption> list = new();

            foreach (int opacity in OpacityList)
            {
                list.Add(new FloatMenuOption($"{opacity}%", () =>
                {
                    MaterialsManager.SetPlanOpacity(planDesignationType, opacity);
                }));
            }

            return list;
        }

        List<FloatMenuOption> GetTextureSetMenuOptions(PlanDesignationType planDesignationType)
        {
            List<FloatMenuOption> list = new();

            foreach (PlanTextureSet planTextureSet in PlanTextureSets)
            {
                list.Add(new FloatMenuOption(planTextureSet.ToString(), () =>
                {
                    MaterialsManager.SetPlanTextureSet(planDesignationType, planTextureSet);
                }));
            }

            return list;
        }
    }
}
