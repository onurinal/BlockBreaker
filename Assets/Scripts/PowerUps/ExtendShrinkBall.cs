using System.Collections;
using BlockBreaker.Balls;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class ExtendShrinkBall
    {
        private Ball ball;
        private Transform ballModelTransform;
        private float extendShrinkAmount;
        private IEnumerator extendCoroutine;
        private IEnumerator shrinkCoroutine;

        public void Initialize(Ball ball, Transform ballModelTransform)
        {
            this.ball = ball;
            this.ballModelTransform = ballModelTransform;
        }

        private IEnumerator ExtendShrinkBallSize(PowerUpProperties powerUpProperties, float scaleAmount)
        {
            extendShrinkAmount += scaleAmount;
            extendShrinkAmount = Mathf.Clamp(extendShrinkAmount, powerUpProperties.minBallExtendShrinkAmount, powerUpProperties.maxBallExtendShrinkAmount);
            var currentScaleX = ballModelTransform.transform.localScale.x;
            var targetScaleX = powerUpProperties.ballBaseSizeModelX + extendShrinkAmount; // only need x info then you will scale x and y at the same time for ball
            var elapsedTime = 0f;
            while (elapsedTime <= powerUpProperties.ballScaleUpDuration)
            {
                if (ballModelTransform.transform.localScale.x < powerUpProperties.minBallSizeModelX || ballModelTransform.transform.localScale.x > powerUpProperties.maxBallSizeModelX)
                {
                    break;
                }

                elapsedTime += Time.deltaTime;
                var newScale = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / powerUpProperties.ballScaleUpDuration);
                ballModelTransform.transform.localScale = new Vector3(newScale, newScale, ballModelTransform.transform.localScale.z);
                yield return null;
            }

            // after the power up make it flat numbers of size
            ballModelTransform.transform.localScale = new Vector3(targetScaleX, targetScaleX, ballModelTransform.transform.localScale.z);
            // after some seconds return the original size
            yield return new WaitForSeconds(powerUpProperties.powerUpDuration);
            ballModelTransform.transform.localScale = new Vector3(powerUpProperties.ballBaseSizeModelX, powerUpProperties.ballBaseSizeModelY, ballModelTransform.transform.localScale.z);
            extendShrinkAmount = 0f;
            extendCoroutine = null;
            shrinkCoroutine = null;
        }

        public void StartExtendBallSize(PowerUpProperties powerUpProperties)
        {
            TryToStopBallExtensionCoroutines();
            extendCoroutine = ExtendShrinkBallSize(powerUpProperties, powerUpProperties.perExtendBallSizeModelX);
            ball.StartCoroutine(extendCoroutine);
        }

        public void StartShrinkBallSize(PowerUpProperties powerUpProperties)
        {
            TryToStopBallExtensionCoroutines();
            shrinkCoroutine = ExtendShrinkBallSize(powerUpProperties, powerUpProperties.perShrinkBallSizeModelX);
            ball.StartCoroutine(shrinkCoroutine);
        }

        public void TryToStopBallExtensionCoroutines()
        {
            if (extendCoroutine != null)
            {
                ball.StopCoroutine(extendCoroutine);
                extendCoroutine = null;
            }

            if (shrinkCoroutine != null)
            {
                ball.StopCoroutine(shrinkCoroutine);
                shrinkCoroutine = null;
            }
        }
    }
}