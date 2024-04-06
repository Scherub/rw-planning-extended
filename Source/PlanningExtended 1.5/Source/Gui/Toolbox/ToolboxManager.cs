using PlanningExtended.Gui.Toolbox.Main.Windows;
using Verse;

namespace PlanningExtended.Gui.Toolbox
{
    internal class ToolboxManager
    {
        public static bool IsToolboxVisible { get; private set; }

        public static void ShowToolboxWindow(bool showWindow)
        {
            if (showWindow)
                ShowToolboxWindow();
            else
                CloseToolboxWindow();
        }

        public static void ShowToolboxWindow()
        {
            MainToolboxWindow toolboxWindow = Find.WindowStack.WindowOfType<MainToolboxWindow>();

            if (toolboxWindow == null)
                Find.WindowStack.Add(new MainToolboxWindow());

            IsToolboxVisible = true;
        }

        public static void CloseToolboxWindow()
        {
            Find.WindowStack.WindowOfType<MainToolboxWindow>()?.Close();

            IsToolboxVisible = false;
        }
    }
}
