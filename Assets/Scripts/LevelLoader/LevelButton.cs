using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlockBreaker.LevelLoader
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        private string levelToLoad;

        public void SetLevelButton(string levelText, string levelToLoad)
        {
            this.levelText.text = levelText;
            this.levelToLoad = levelToLoad;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}