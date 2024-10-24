using System.Collections;
using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class InvisiblePaddle
    {
        private Paddle paddle;
        private IEnumerator invisiblePaddleCoroutine;
        private Renderer renderer;

        public void Initialize(Paddle paddle)
        {
            this.paddle = paddle;
            renderer = paddle.GetComponentInChildren<Renderer>();
        }

        private IEnumerator InvisiblePaddleObject(PowerUpProperties powerUpProperties)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(powerUpProperties.powerUpDuration);
            renderer.enabled = true;
            invisiblePaddleCoroutine = null;
        }

        public void StartInvisiblePaddle(PowerUpProperties powerUpProperties)
        {
            TryToStopInvisiblePaddle();
            invisiblePaddleCoroutine = InvisiblePaddleObject(powerUpProperties);
            paddle.StartCoroutine(invisiblePaddleCoroutine);
        }

        public void TryToStopInvisiblePaddle()
        {
            if (invisiblePaddleCoroutine != null)
            {
                paddle.StopCoroutine(invisiblePaddleCoroutine);
                renderer.enabled = true;
                invisiblePaddleCoroutine = null;
            }
        }
    }
}