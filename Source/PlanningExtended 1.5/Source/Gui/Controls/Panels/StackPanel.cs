using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;

namespace PlanningExtended.Gui.Controls
{
    //internal class StackPanel : Panel
    //{
    //    public Orientation Orientation { get; }

    //    public StackPanel(GridPosition? gridPosition = null, Thickness? margin = null, Orientation orientation = Orientation.Vertical)
    //        : base(gridPosition, margin)
    //    {
    //        Orientation = orientation;
    //    }

    //    protected override Size OnMeasure(Size availableSize)
    //    {
    //        Size availableStackSize = availableSize;
    //        Size desiredStackSize = Size.Zero;

    //        foreach (BaseControl child in Children)
    //        {
    //            Size desiredChildSize = child.Measure(availableStackSize);

    //            if (Orientation == Orientation.Horizontal)
    //            {
    //                desiredStackSize.Width += desiredChildSize.Width;
    //                desiredStackSize.Height = Mathf.Max(desiredStackSize.Height, desiredChildSize.Height);
    //                availableStackSize.Width -= desiredChildSize.Width;
    //            }
    //            else
    //            {
    //                desiredStackSize.Height += desiredChildSize.Height;
    //                desiredStackSize.Width = Mathf.Max(desiredStackSize.Width, desiredChildSize.Width);
    //                availableStackSize.Height -= desiredChildSize.Height;
    //            }
    //        }

    //        return desiredStackSize;
    //    }

    //    protected override void OnDraw(Rect rect)
    //    {
    //        //Arrange(rect);

    //        //foreach (BaseControl child in Children)
    //        //    child.Draw(GetRect(child.GridPosition));
    //    }
    //}
}
