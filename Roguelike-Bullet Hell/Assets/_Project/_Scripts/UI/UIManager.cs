using System.Collections.Generic;
using Project.Singleton;

namespace Project.Gameplay.UI
{
    /// <summary>
    /// Global UI controller responsible for panel navigation,
    /// opening and closing panels, maintaining the open panels,
    /// and handling back/escape input.
    /// </summary>
    public class UIManager : MonoBehaviourSingleton<UIManager>
    {
        private readonly List<UIPanel> openPanels = new();

        /// <summary>
        /// Opens the specified panel and adds it to the UI navigation stack.
        /// </summary>
        public static void Open(UIPanel panel)
        {
            Instance.InternalOpen(panel);
        }

        /// <summary>
        /// Closes the specified panel and removes it from the UI navigation stack.
        /// </summary>
        public static void Close(UIPanel panel)
        {
            Instance.InternalClose(panel);
        }

        /// <summary>
        /// Closes the top-most panel in the UI navigation stack.
        /// </summary>
        public static void Close()
        {
            Instance.InternalClose();
        }

        private void InternalOpen(UIPanel panel)
        {
            if(openPanels.Contains(panel)) return;

            panel.Open();
            openPanels.Add(panel);
        }

        private void InternalClose(UIPanel panel)
        {
            if(!openPanels.Remove(panel)) return;

            panel.Close();
        }

        private void InternalClose()
        {
            if (openPanels.Count <= 0) return;

            UIPanel panel = openPanels[^1];
            openPanels.Remove(panel);
            panel.Close();
        }
    }
}