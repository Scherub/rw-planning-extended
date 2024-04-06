using PlanningExtended.Gui.Controls;
using PlanningExtended.Gui.Controls.Grid;
using PlanningExtended.Gui.Toolbox.Main.Controls.Panels;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Toolbox.Main.Windows
{
    public class MainToolboxWindow : Window
    {
        public override Vector2 InitialSize => new(460f, 480f);

        readonly GridPanel gridPanel = new("*", "24, 12, 48, 12, 128");

        protected override float Margin => 2f;

        public MainToolboxWindow()
        {
            layer = WindowLayer.GameUI;
            draggable = true;
            closeOnAccept = false;
            closeOnCancel = false;
            focusWhenOpened = false;
            preventCameraMotion = false;

            gridPanel.Children.Add(new TitleBarPanel(GridPosition.Zero));
            gridPanel.Children.Add(new ToolBarPanel(GridPosition.StartIndex(0, 2)));
            gridPanel.Children.Add(new PlanButtonsToolBarPanel(GridPosition.StartIndex(0, 4)));
        }

        protected override void SetInitialSizeAndPosition()
        {
            Vector2 windowPosition = PlanningMod.Settings.Toolbox.GetWindowPosition();

            if (windowPosition == Vector2.zero)
                base.SetInitialSizeAndPosition();
            else
            {
                windowPosition = GetCorrectedWindowPosition(windowPosition, InitialSize);

                windowRect = new Rect(windowPosition.x, windowPosition.y, InitialSize.x, InitialSize.y);
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            gridPanel.Draw(inRect);
        }

        public override void PreClose()
        {
            base.PreClose();

            PlanningMod.Settings.Toolbox.SetWindowPosition(new Vector2(windowRect.x, windowRect.y));
        }

        Vector2 GetCorrectedWindowPosition(Vector2 windowPosition, Vector2 windowSize)
        {
            if (windowPosition.x < 0)
                windowPosition.x = 0;
            else if (windowPosition.x + windowSize.x > UI.screenWidth)
                windowPosition.x = UI.screenWidth - windowSize.x;
            if (windowPosition.y < 0)
                windowPosition.y = 0;
            else if (windowPosition.y + windowSize.y > UI.screenHeight)
                windowPosition.y = UI.screenHeight - windowSize.y;

            return windowPosition;
        }
    }
}
