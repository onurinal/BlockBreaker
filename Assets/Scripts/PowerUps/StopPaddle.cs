using System.Collections;
using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class StopPaddleMovement
    {
        private Paddle paddle;
        private bool isStopPaddlePowerOn;
        private IEnumerator stopPaddleMovementCoroutine;

        public void Initialize(Paddle paddle)
        {
            this.paddle = paddle;
        }

        private IEnumerator StopPaddleMovements(PowerUpProperties powerUpProperties)
        {
            isStopPaddlePowerOn = true;
            yield return new WaitForSeconds(powerUpProperties.powerUpDuration);
            isStopPaddlePowerOn = false;
            stopPaddleMovementCoroutine = null;
        }

        public void StartToStopPaddleMovement(PowerUpProperties powerUpProperties)
        {
            TryToStopPaddleMovementCoroutine();
            stopPaddleMovementCoroutine = StopPaddleMovements(powerUpProperties);
            paddle.StartCoroutine(stopPaddleMovementCoroutine);
        }

        public void TryToStopPaddleMovementCoroutine()
        {
            if (stopPaddleMovementCoroutine != null)
            {
                paddle.StopCoroutine(stopPaddleMovementCoroutine);
                isStopPaddlePowerOn = false;
                stopPaddleMovementCoroutine = null;
            }
        }

        public bool IsStopPaddlePowerOn()
        {
            return isStopPaddlePowerOn;
        }
    }
}