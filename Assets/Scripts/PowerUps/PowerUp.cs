using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpType powerUpType;
        [SerializeField] private PowerUpProperties powerUpProperties;

        private void ApplyPowerUp()
        {
            GameManager.Instance.PowerUpCollected(powerUpType, powerUpProperties);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<Paddle>() != null)
            {
                ApplyPowerUp();
                Destroy(gameObject);
            }
        }
    }
}