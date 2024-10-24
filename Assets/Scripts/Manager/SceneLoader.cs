using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlockBreaker.Manager
{
    public class SceneLoader : MonoBehaviour
    {
        private int currentSceneIndex;

        public static SceneLoader Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LoadNextScene()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void LoadLevelMenu()
        {
            SceneManager.LoadScene("Level Menu");
        }

        public void LoadGameOver()
        {
            SceneManager.LoadScene("Game Over");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}