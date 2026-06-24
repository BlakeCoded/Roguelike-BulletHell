using Project.Systems.Keybinds;
using UnityEngine.TextCore.Text;

namespace Project.Gameplay.UI
{
    public class PlayerTabPanel : UIPanel
    {
        private void Awake()
        {
            InputManager.Subscribe(HandleInput);
            gameObject.SetActive(false);
        }

        public void HandleInput(string actionID)
        {
            if (actionID == InputIDs.Character)
            {
                Toggle();
            }
        }

        private void OnDestroy()
        {
            InputManager.Unsubscribe(HandleInput);
        }
    }
}