using PlanningExtended.Designations;
using System.Collections.Generic;
using System;
using Verse;
using RimWorld;

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
                return GetPlanTypeMenuOptions((planDesignationType) =>
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

        protected List<FloatMenuOption> GetPlanTypeMenuOptions(Action<PlanDesignationType> action)
        {
            List<FloatMenuOption> list = new();

            foreach (PlanDesignationType planDesignationType in PlanDesignationUtilities.GetPlanDesignationTypes())
            {
                list.Add(new FloatMenuOption(planDesignationType.ToString(), () =>
                {
                    action(planDesignationType);
                }));
            }

            return list;
        }
    }
}
