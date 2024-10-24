using System.Collections;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class ExtendShrinkPaddle
    {
        private float paddleExtendShrinkAmount;
        private IEnumerator paddleExtendCoroutine, paddleShrinkCoroutine;
        private Paddle paddle;
        private Transform paddleModelTransform;

        private PaddleProperties paddleProperties;
        private Transform laserBulletLeftSpawn, laserBulletRightSpawn;
        private Transform wallDropLeftSpawn, wallDropRightSpawn;

        public void Initialize(Paddle paddle, Transform paddleModelTransform, Transform laserBulletLeftSpawn, Transform laserBulletRightSpawn, PaddleProperties paddleProperties,
            GameObject wallDropLeftSpawn, GameObject wallDropRightSpawn)
        {
            this.paddle = paddle;
            this.paddleModelTransform = paddleModelTransform;
            this.laserBulletLeftSpawn = laserBulletLeftSpawn;
            this.laserBulletRightSpawn = laserBulletRightSpawn;
            this.paddleProperties = paddleProperties;
            this.wallDropLeftSpawn = wallDropLeftSpawn.transform;
            this.wallDropRightSpawn = wallDropRightSpawn.transform;
        }

        // -------------------- PADDLE EXTEND/SHRINK POWER UP ----------------------
        private IEnumerator ExtendShrinkPaddleSize(PowerUpProperties powerUpProperties, float scaleAmount)
        {
            paddleExtendShrinkAmount += scaleAmount; // getting info how much paddle will scale for taking two or more power up at the same time
            paddleExtendShrinkAmount = Mathf.Clamp(paddleExtendShrinkAmount, powerUpProperties.minPaddleExtendShrinkAmount, powerUpProperties.maxPaddleExtendShrinkAmount);
            var currentScaleX = paddleModelTransform.transform.localScale.x;
            var targetScaleX = powerUpProperties.paddleBaseSizeModelX + paddleExtendShrinkAmount;
            var elapsedTime = 0f;
            while (elapsedTime <= powerUpProperties.paddleScaleUpDuration)
            {
                if (paddleModelTransform.transform.localScale.x < powerUpProperties.minPaddleSizeModelX || paddleModelTransform.transform.localScale.x > powerUpProperties.maxPaddleSizeModelX)
                {
                    break;
                }

                elapsedTime += Time.deltaTime;
                var newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / powerUpProperties.paddleScaleUpDuration);
                paddleModelTransform.transform.localScale = new Vector3(newScaleX, paddleModelTransform.transform.localScale.y, paddleModelTransform.transform.localScale.z);
                yield return null;
            }

            paddleModelTransform.transform.localScale = new Vector3(targetScaleX, paddleModelTransform.transform.localScale.y, paddleModelTransform.transform.localScale.z);
            UpdateLaserAndWallDropSpawnPositions();
            yield return new WaitForSeconds(powerUpProperties.powerUpDuration);

            // RESET TO ORIGINAL STATE
            paddleModelTransform.transform.localScale = new Vector3(paddleProperties.paddleBaseSizeModelX, paddleModelTransform.transform.localScale.y, paddleModelTransform.transform.localScale.z);
            ResetPaddleExtendShrinkAmount();
            ResetLaserAndWallDropSpawnPositions();
            paddleExtendCoroutine = null;
            paddleShrinkCoroutine = null;
        }

        public void StartExtendPaddleSize(PowerUpProperties powerUpProperties)
        {
            TryToStopPaddleExtensionCoroutines();

            paddleExtendCoroutine = ExtendShrinkPaddleSize(powerUpProperties, powerUpProperties.perExtendPaddleSizeModelX);
            paddle.StartCoroutine(paddleExtendCoroutine);
        }

        public void StartShrinkPaddleSize(PowerUpProperties powerUpProperties)
        {
            TryToStopPaddleExtensionCoroutines();

            paddleShrinkCoroutine = ExtendShrinkPaddleSize(powerUpProperties, powerUpProperties.perShrinkPaddleSizeModelX);
            paddle.StartCoroutine(paddleShrinkCoroutine);
        }

        private void TryToStopPaddleExtensionCoroutines()
        {
            if (paddleExtendCoroutine != null)
            {
                paddle.StopCoroutine(paddleExtendCoroutine);
                paddleExtendCoroutine = null;
            }

            if (paddleShrinkCoroutine != null)
            {
                paddle.StopCoroutine(paddleShrinkCoroutine);
                paddleShrinkCoroutine = null;
            }
        }

        private void ResetPaddleExtendShrinkAmount()
        {
            paddleExtendShrinkAmount = 0f;
        }

        public void TryToResetAllExtensions()
        {
            TryToStopPaddleExtensionCoroutines();
            ResetPaddleExtendShrinkAmount();
        }

        private void UpdateLaserAndWallDropSpawnPositions(Transform leftTransform, Transform rightTransform, float leftBasePosition, float rightBasePosition)
        {
            var newLeftPowerUpSpawnPosition = leftTransform.localPosition;
            newLeftPowerUpSpawnPosition.x = paddleModelTransform.localScale.x / (paddleProperties.paddleBaseSizeModelX / leftBasePosition);
            leftTransform.localPosition = newLeftPowerUpSpawnPosition;
            var newRightPowerUpSpawnPosition = rightTransform.localPosition;
            newRightPowerUpSpawnPosition.x = paddleModelTransform.localScale.x / (paddleProperties.paddleBaseSizeModelX / rightBasePosition);
            rightTransform.transform.localPosition = newRightPowerUpSpawnPosition;
        }

        private void UpdateLaserAndWallDropSpawnPositions()
        {
            UpdateLaserAndWallDropSpawnPositions(laserBulletLeftSpawn, laserBulletRightSpawn, paddleProperties.paddleLaserLeftSpawnBasePositionX, paddleProperties.paddleLaserRightSpawnBasePositionX);
            UpdateLaserAndWallDropSpawnPositions(wallDropLeftSpawn, wallDropRightSpawn, paddleProperties.paddleWallDropLeftSpawnBasePositionX, paddleProperties.paddleWallDropRightSpawnBasePositionX);
        }

        private void ResetLaserAndWallDropSpawnPositions(Transform leftTransform, Transform rightTransform, float positionModifier)
        {
            var newLeftPowerUpSpawnPosition = leftTransform.transform.localPosition;
            newLeftPowerUpSpawnPosition.x = -paddleProperties.paddleBaseSizeModelX * positionModifier;
            leftTransform.transform.localPosition = newLeftPowerUpSpawnPosition;
            var newRightPowerUpSpawnPositions = rightTransform.transform.localPosition;
            newRightPowerUpSpawnPositions.x = paddleProperties.paddleBaseSizeModelX * positionModifier;
            rightTransform.transform.localPosition = newRightPowerUpSpawnPositions;
        }

        public void ResetLaserAndWallDropSpawnPositions()
        {
            ResetLaserAndWallDropSpawnPositions(laserBulletLeftSpawn, laserBulletRightSpawn, paddleProperties.paddleLaserSpawnPositionModifier);
            ResetLaserAndWallDropSpawnPositions(wallDropLeftSpawn, wallDropRightSpawn, paddleProperties.paddleWallDropSpawnPositionModifier);
        }
    }
}