using UnityEngine;
using UnityEngine.UI;

namespace BlockBreaker.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public Image winScreenPanel;
        [SerializeField] private GameObject[] playerLifeIcon = new GameObject[3];

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void RemoveLifeIcon(int currentPlayerLife)
        {
            playerLifeIcon[currentPlayerLife].SetActive(false);
        }
    }
}