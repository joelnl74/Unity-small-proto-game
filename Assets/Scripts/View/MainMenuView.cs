using UnityEngine;

namespace Game
{
    /// <summary>
    /// Main menu view logic.
    /// </summary>
    public class MainMenuView : MonoBehaviour
    , IMainMenuView
    {
        public void ToggleView(bool active)
            => gameObject.SetActive(active);
    }
}
