using System.Collections.Generic;
using BlockBreaker.Balls;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class BallPowerUpController
    {
        private Ball ball;
        private Transform ballModelTransform;
        private ExtendShrinkBall extendShrinkBall;

        public void Initialize(Ball ball, Transform ballModelTransform)
        {
            this.ball = ball;
            this.ballModelTransform = ballModelTransform;
            extendShrinkBall = new ExtendShrinkBall();
            extendShrinkBall.Initialize(ball, ballModelTransform);
        }

        public void ApplyPowerUp(PowerUpType powerUpType, PowerUpProperties powerUpProperties)
        {
            switch (powerUpType)
            {
                case PowerUpType.ExtendBallSize:
                    extendShrinkBall.StartExtendBallSize(powerUpProperties);
                    break;
                case PowerUpType.ShrinkBallSize:
                    extendShrinkBall.StartShrinkBallSize(powerUpProperties);
                    break;
            }
        }

        public void TryToStopBallExtensionCoroutines()
        {
            extendShrinkBall.TryToStopBallExtensionCoroutines();
        }
    }
}