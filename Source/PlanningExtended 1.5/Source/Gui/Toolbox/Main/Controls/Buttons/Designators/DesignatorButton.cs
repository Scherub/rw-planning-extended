using PlanningExtended.Defs;
using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Controls.Buttons.Designators
{
    internal class DesignatorButton<TDesignator> : IconButton
        where TDesignator : Designator
    {
        public TDesignator Designator { get; }

        public override bool IsEnabled => !Designator.Disabled;

        public override Color IconDrawColor => Designator.IconDrawColor;

        public DesignatorButton(GridPosition? gridPosition = null, Thickness? margin = null)
            : base((Texture2D)null, gridPosition, margin)
        {
            Designator = DefDesignatorResolver.Resolve<TDesignator>();
            IconTexture = Designator.icon as Texture2D;

            OnClick = () => Find.DesignatorManager.Select(Designator);
        }
    }
}
