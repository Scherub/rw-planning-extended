using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;

namespace PlanningExtended.Gui.Controls
{
    internal abstract class BaseControl
    {
        Rect _rect;

        public virtual bool IsEnabled { get; set; } = true;

        public Visibility Visibility { get; set; }

        public bool IsVisible => Visibility == Visibility.Visible;

        public bool IsCollapsed => Visibility == Visibility.Collapsed;

        public GridPosition GridPosition { get; }

        public Thickness Margin { get; set; }

        public float Width { get; set; } = float.PositiveInfinity;

        public float Height { get; set; } = float.PositiveInfinity;

        internal Size DesiredSize { get; set; }

        protected BaseControl(GridPosition? gridPosition = null, Thickness? margin = null)
        {
            GridPosition = gridPosition ?? GridPosition.Zero;
            Margin = margin ?? Thickness.Zero;
        }

        public void MeasureAndArrange(Rect rect)
        {
            if (_rect == rect)
                return;

            _rect = rect;

            Measure(new Size(rect.width, rect.height));

        }

        public void Draw(Rect rect)
        {
            //if (_rect != rect)
            //{
            //    Measure(new Size(rect.width, rect.height));
            //    _rect = rect;
            //}

            OnDraw(rect);
        }

        public Size Measure(Size availableSize)
        {
            Size newAvailableSize = new(availableSize.Width - Margin.Width, availableSize.Height - Margin.Height);

            DesiredSize = OnMeasure(newAvailableSize);

            return DesiredSize;
        }

        protected abstract void OnDraw(Rect rect);

        protected virtual Size OnMeasure(Size availableSize)
        {
            //float width = float.IsPositiveInfinity(Width) ? availableSize.Width : Width;
            //float height = float.IsPositiveInfinity(Height) ? availableSize.Height : Height;
            float width = float.IsPositiveInfinity(Width) ? 0f : Width;
            float height = float.IsPositiveInfinity(Height) ? 0f : Height;

            return new Size(width, height);
        }
    }
}
