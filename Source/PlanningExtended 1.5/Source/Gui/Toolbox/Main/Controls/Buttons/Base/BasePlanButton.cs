using System;
using PlanningExtended.Defs;
using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons
{
    internal class BasePlanButton<TDesignator> : IconButton
        where TDesignator : Designator
    {
        public TDesignator Designator { get; }

        public BasePlanButton(string texturePath, GridPosition? gridPosition = null, Thickness? margin = null, bool showBorder = true, float iconScale = 0.85F, Action onClick = null)
            : base(texturePath, gridPosition, margin, showBorder, iconScale, onClick)
        {
            Designator = DefDesignatorResolver.Resolve<TDesignator>();
        }
    }
}
