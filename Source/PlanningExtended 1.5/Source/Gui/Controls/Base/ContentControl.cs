using PlanningExtended.Gui.Controls.Grid;

namespace PlanningExtended.Gui.Controls
{
    internal abstract class ContentControl : BaseControl
    {
        protected BaseControl Child { get; private set; }

        public Thickness Padding
        {
            get => Child.Margin;
            set => Child.Margin = value;
        }

        protected ContentControl(BaseControl child, GridPosition? gridPosition = default, Thickness? margin = default, Thickness? padding = default)
            : base(gridPosition, margin)
        {
            Child = child;
            Padding = padding ?? Thickness.Zero;
        }

        protected override Size OnMeasure(Size availableSize)
        {
            return Child.Measure(availableSize);
        }
    }
}
