using System.Collections.Generic;
using PlanningExtended.Gui.Utilities;
using Verse;

namespace PlanningExtended.Designators
{
    public class ChangePlanAppearanceDesignator : BasePlanMenuDesignator
    {
        public override bool Visible => PlanningMod.Settings.General.displayChangePlanAppearanceDesignator;

        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetMenuOptions((planDesignationType) => GuiMenuOptionsUtilities.GetTextureSetsMenuOptions(planDesignationType));

        public ChangePlanAppearanceDesignator()
            : base("ChangePlanAppearance")
        {
        }

        public override void Click()
        {
            List<FloatMenuOption> list = GetMenuOptions((planDesignationType) => GuiMenuOptionsUtilities.GetOpacityMenuOptions(planDesignationType));

            Find.WindowStack.Add(new FloatMenu(list));
        }
    }
}
