using System.Collections;
using BlockBreaker.Manager;
using UnityEngine;

namespace BlockBreaker.Blocks
{
    public class BreakableBlock : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxBlockLife;
        [SerializeField] private int pointsPerBlockDestroyed;
        [SerializeField] private GameObject particlePrefab;
        [SerializeField] private Sprite[] blockSpriteList; // to change sprites if block take damage

        // block hit animation variables
        [SerializeField] private Transform blockModelTransform;
        [SerializeField] private float blockScaleAmount;
        [SerializeField] private float scaleBlockSpeed;

        private int currentBlockLife;
        private bool isBlockAlive = true;
        private SpriteRenderer blockSprite;

        private void Awake()
        {
            currentBlockLife = maxBlockLife;
        }

        private void Start()
        {
            GameManager.Instance.CountBreakableBlock(this);
            blockSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void TakeDamage(int damage)
        {
            currentBlockLife -= damage;
            if (currentBlockLife <= 0)
            {
                DestroyBlock();
            }
            else
            {
                var playBlockAnimation = PlayBlockHitAnimation();
                StartCoroutine(playBlockAnimation);
                ShowNextBlockSprite();
            }
        }

        private void ShowNextBlockSprite()
        {
            var spriteIndex = currentBlockLife - 1;
            if (blockSpriteList[spriteIndex] != null)
            {
                blockSprite.sprite = blockSpriteList[spriteIndex];
            }
        }

        private void DestroyBlock()
        {
            if (!isBlockAlive)
            {
                return;
            }

            isBlockAlive = false;
            GameManager.Instance.RemoveBreakableBlock(this);
            ScoreManager.Instance.AddScore(pointsPerBlockDestroyed);
            PowerUpManager.Instance.DropPowerUp(transform.position);
            Destroy(gameObject);
            PlayParticleBlock();
        }

        private void PlayParticleBlock()
        {
            var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(particle, 1f);
        }

        // ---------------------------- BLOCK HIT ANIMATION --------------------------------
        private IEnumerator BlockHitAnimation(float scaleAmount)
        {
            var currentScale = 0f;
            var currentBlockSize = blockModelTransform.transform.localScale.x; // we need x position only, then scale x and y position equally
            while (Mathf.Abs(scaleAmount) - Mathf.Abs(currentScale) > 0)
            {
                currentScale += Time.deltaTime * scaleBlockSpeed * Mathf.Sign(scaleAmount);
                var newScale = currentScale + currentBlockSize;
                blockModelTransform.transform.localScale = new Vector3(newScale, newScale, blockModelTransform.transform.localScale.z);
                yield return null;
            }

            var finalScale = currentBlockSize + scaleAmount;
            blockModelTransform.transform.localScale = new Vector3(finalScale, finalScale, blockModelTransform.transform.localScale.z);
        }

        private IEnumerator PlayBlockHitAnimation()
        {
            yield return (BlockHitAnimation(-blockScaleAmount));
            yield return (BlockHitAnimation(blockScaleAmount));
        }
    }
}