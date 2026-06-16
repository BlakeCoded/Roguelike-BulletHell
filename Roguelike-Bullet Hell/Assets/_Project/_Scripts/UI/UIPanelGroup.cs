using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Gameplay.UI
{
    /// <summary>
    /// Controls a collection of UI panels that should be opened
    /// and closed together, such as a gameplay HUD.
    /// </summary>
    public class UIPanelGroup : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> panels = new();

        public void Open()
        {
            for(int i = 0; i < panels.Count; i++)
            {
                UIManager.Open(panels[i]);
            }
        }

        public void Close()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                UIManager.Close(panels[i]);
            }
        }
    }
}