using System;
using System.Collections.Generic;
using PlanningExtended.Gui.Utilities;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BasePlanMenuDesignator : BaseClickDesignator
    {
        protected bool IsPlanMenuKeyPressed => PlanningKeyBindingDefOf.Planning_ColorPicker?.IsDown == true;

        protected BasePlanMenuDesignator(string name)
            : base(name)
        {
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate(PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel);
        }

        protected BasePlanMenuDesignator(string name, string iconName) 
            : base(name, iconName)
        {
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate(PlanningKeyBindingDefOf.Planning_ColorPicker.MainKeyLabel);
        }

        protected List<FloatMenuOption> GetMenuOptions(Func<PlanDesignationType, List<FloatMenuOption>> getMenuOptions)
        {
            if (IsPlanMenuKeyPressed)
            {
                return GuiMenuOptionsUtilities.GetPlanTypeMenuOptions((planDesignationType) =>
                {
                    List<FloatMenuOption> list = getMenuOptions(planDesignationType);

                    Find.WindowStack.Add(new FloatMenu(list));
                });
            }
            else
            {
                return getMenuOptions(PlanDesignationType.Unknown);
            }
        }
    }
}
