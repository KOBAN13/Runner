using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Finish
{
    public class FinishMenu : MonoBehaviour
    {
        [field: SerializeField] public Button ButtonMenuChoiceLevel { get; private set; }
        [field: SerializeField] public Button ExitGame { get; private set; }

        public void Awake()
        {
            ListenerButton();
        }

        private void ListenerButton()
        {
            ButtonMenuChoiceLevel.onClick.AddListener(OnChoiceLevelMenu);
            ExitGame.onClick.AddListener(OnExitGame);
        }

        private void OnExitGame() => Application.Quit();

        private void OnChoiceLevelMenu() => SceneManager.LoadScene("ChoiceLevel");
    }
    
}