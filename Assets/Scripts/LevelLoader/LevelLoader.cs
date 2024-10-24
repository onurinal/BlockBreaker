using UnityEngine;
using UnityEngine.UI;

namespace BlockBreaker.LevelLoader
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Button levelButton;
        [SerializeField] private string[] levels;

        private void Start()
        {
            CreateLevelButton();
        }

        private void CreateLevelButton()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                var newButton = Instantiate(levelButton, transform);
                levels[i] = (i + 1).ToString();
                newButton.GetComponent<LevelButton>().SetLevelButton((i + 1).ToString(), levels[i]);
            }
        }
    }
}