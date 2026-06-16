using Project.Systems.Keybinds;

namespace Project.Gameplay.UI
{
    public class PlayerTabPanel : UIPanel
    {
        public void HandleInput(string actionID)
        {
            if (actionID == InputIDs.Character)
            {
                Toggle();
            }
        }
    }
}