using System;
using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Shapes;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Designators.Shapes.ExtraControls
{
    internal class ShapeExtraControlManager
    {
        readonly List<BaseShapeExtraControlWidget> _shapeExtraContols = new() { new DisplayShapeVariantExtraControlWidget(), new ShapeRotationExtraControlWidget(), new ShapeSegmentExtraControlWidget() };

        public void DrawExtraControls(float leftX, float bottomY, BaseShape shape)
        {
            float width = 200f;
            float height = GetTotalRequiredWidgetHeight(shape.ShapeDisplayOptions);

            Rect winRect = new(leftX, bottomY - height, width, height);

            Find.WindowStack.ImmediateWindow(73095, winRect, WindowLayer.GameUI, () =>
            {
                float widgetStartY = height;

                TextAnchor originalAnchor = Text.Anchor;
                GameFont originalFont = Text.Font;

                int i = 0;

                foreach (var extraControlWidget in GetWidgets(shape.ShapeDisplayOptions))
                {
                    if (i == 1)
                        Widgets.DrawLineHorizontal(winRect.x + 5f, widgetStartY, winRect.width - 10f);

                    widgetStartY -= extraControlWidget.RequiredHeight;

                    Rect widgetRect = new(winRect.x, widgetStartY, winRect.width, extraControlWidget.RequiredHeight);

                    extraControlWidget.Draw(widgetRect, shape);

                    i++;
                }

                Text.Anchor = originalAnchor;
                Text.Font = originalFont;
            }, true, false, 1f, null);
        }

        IEnumerable<BaseShapeExtraControlWidget> GetWidgets(ShapeDisplayOptions shapeDisplayOptions)
        {
            var _shapeDisplayOptions = Enum.GetValues(typeof(ShapeDisplayOptions));

            foreach (ShapeDisplayOptions shapeDisplayOption in _shapeDisplayOptions)
            {
                if (shapeDisplayOptions.HasFlag(shapeDisplayOption))
                {
                    BaseShapeExtraControlWidget shapeExtraControl = _shapeExtraContols.FirstOrDefault(sec => sec.ShapeDisplayOption == shapeDisplayOption);

                    if (shapeExtraControl != null)
                        yield return shapeExtraControl;
                }
            }
        }

        float GetTotalRequiredWidgetHeight(ShapeDisplayOptions shapeDisplayOptions)
        {
            float requiredHeight = 0f;

            foreach (var extraControl in GetWidgets(shapeDisplayOptions))
                requiredHeight += extraControl.RequiredHeight;

            return requiredHeight;
        }
    }
}
