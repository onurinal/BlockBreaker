using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Manager
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> powerUps = new List<GameObject>();
        [Range(0, 100)] [SerializeField] private int dropPowerUpChance;

        public static PowerUpManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void DropPowerUp(Vector3 blockPosition)
        {
            var percentage = Random.Range(1, 101);
            if (percentage <= dropPowerUpChance)
            {
                var powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Count)], blockPosition, Quaternion.identity);
            }
        }
    }
}