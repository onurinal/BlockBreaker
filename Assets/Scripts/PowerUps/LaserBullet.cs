using BlockBreaker.Blocks;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class LaserBullet : MonoBehaviour
    {
        [SerializeField] private PowerUpProperties powerUpProperties;
        [SerializeField] private Rigidbody2D myRigidbody2D;
        [SerializeField] private LayerMask layerToDestroyWhenHit;

        private void Start()
        {
            myRigidbody2D.velocity = new Vector2(0f, powerUpProperties.laserBulletSpeedY);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var laserCollision = collision.gameObject.GetComponent<IDamageable>();
            if (laserCollision != null)
            {
                laserCollision.TakeDamage(powerUpProperties.laserBulletDamage);
                Destroy(gameObject);
            }

            if ((1 << collision.gameObject.layer & layerToDestroyWhenHit) != 0)
            {
                Destroy(gameObject);
            }

            // SECOND WAY TO DESTROY LASER BULLETS WHEN YOU HIT UNBREAKABLE BLOCKS OR WALLS
            // if (collision.gameObject.layer == LayerMask.NameToLayer("UnbreakableBlock") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            // {
            //     Destroy(gameObject);
            // }
        }
    }
}