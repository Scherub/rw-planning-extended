using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Materials;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public class ChangePlanAppearanceDesignator : BaseClickDesignator
    {
        List<int> OpacityList => Enumerable.Range(1, 100).Where(i => i % 10 == 0).ToList();

        public override bool Visible => PlanningMod.Settings.displayChangePlanAppearanceDesignator;

        public ChangePlanAppearanceDesignator()
            : base("ChangePlanAppearance")
        {
        }

        public override void ProcessInput(Event ev)
        {
            ShowOpacityMenu();

        }

        void ShowOpacityMenu()
        {
            List<FloatMenuOption> list = new();

            foreach (int opacity in OpacityList)
            {
                list.Add(new FloatMenuOption($"{opacity}%", () =>
                {
                    MaterialsManager.SetPlanOpacity(opacity);
                }));
            }

            Find.WindowStack.Add(new FloatMenu(list, "Plan Opacity"));
        }
    }
}
