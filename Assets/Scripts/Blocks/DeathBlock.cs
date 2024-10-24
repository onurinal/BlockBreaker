using BlockBreaker.Balls;
using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.Blocks
{
    public class DeathBlock : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D myRigidbody2D;
        private bool isDeathBlockAlive;

        private void Start()
        {
            isDeathBlockAlive = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                myRigidbody2D.velocity = new Vector2(0f, -4f);
            }

            var paddle = collision.gameObject.GetComponent<Paddle>();
            if (paddle != null)
            {
                if (isDeathBlockAlive)
                {
                    GameManager.Instance.RemoveLife();
                    isDeathBlockAlive = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}