using TMPro;
using UnityEngine;

namespace BlockBreaker.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private int currentScore;

        public static ScoreManager Instance;

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

        public void AddScore(int pointsPerBlockDestroy)
        {
            currentScore += pointsPerBlockDestroy;
            scoreText.text = currentScore.ToString();
        }
    }
}