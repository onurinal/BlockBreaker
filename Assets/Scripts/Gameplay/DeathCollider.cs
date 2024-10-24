using BlockBreaker.Manager;
using UnityEngine;
using BlockBreaker.Balls;
using BlockBreaker.PowerUps;

namespace BlockBreaker.Gameplay
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var ball = collision.GetComponentInParent<Ball>();
            var powerUp = collision.GetComponentInParent<PowerUp>();

            if (ball != null)
            {
                GameManager.Instance.RemoveBall(ball);
            }

            if (powerUp != null)
            {
                Destroy(powerUp.gameObject);
            }
        }
    }
}