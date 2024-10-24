using System.Collections.Generic;
using BlockBreaker.Balls;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class PaddlePowerUpController
    {
        private Paddle paddle;
        private Transform paddleModelTransform;
        private ExtendShrinkPaddle extendShrinkPaddle;
        private InvisiblePaddle invisiblePaddle;
        private StopPaddleMovement stopPaddleMovement;
        private Laser laser;
        private WallDrop wallDrop;


        public void Initialize(Paddle paddle, LaserBullet laserBulletPrefab, Transform laserBulletLeftSpawn, Transform laserBulletRightSpawn, Transform paddleModelTransform,
            PaddleProperties paddleProperties, GameObject wallDropLeftSpawn, GameObject wallDropRightSpawn)
        {
            this.paddle = paddle;
            this.paddleModelTransform = paddleModelTransform;
            extendShrinkPaddle = new ExtendShrinkPaddle();
            extendShrinkPaddle.Initialize(paddle, paddleModelTransform, laserBulletLeftSpawn, laserBulletRightSpawn, paddleProperties, wallDropLeftSpawn, wallDropRightSpawn);
            invisiblePaddle = new InvisiblePaddle();
            invisiblePaddle.Initialize(paddle);
            stopPaddleMovement = new StopPaddleMovement();
            stopPaddleMovement.Initialize(paddle);
            laser = new Laser();
            laser.Initialize(paddle, laserBulletPrefab, laserBulletLeftSpawn, laserBulletRightSpawn);
            wallDrop = new WallDrop();
            wallDrop.Initialize(paddle, wallDropLeftSpawn, wallDropRightSpawn);
        }

        public void ApplyPowerUp(PowerUpType powerUpType, PowerUpProperties powerUpProperties, List<Ball> ballList)
        {
            switch (powerUpType)
            {
                case PowerUpType.ExtendPaddleSize:
                    extendShrinkPaddle.StartExtendPaddleSize(powerUpProperties);
                    break;
                case PowerUpType.ShrinkPaddleSize:
                    extendShrinkPaddle.StartShrinkPaddleSize(powerUpProperties);
                    break;
                case PowerUpType.StopPaddleMovement:
                    stopPaddleMovement.StartToStopPaddleMovement(powerUpProperties);
                    break;
                case PowerUpType.InvisiblePaddle:
                    invisiblePaddle.StartInvisiblePaddle(powerUpProperties);
                    break;
                case PowerUpType.Laser:
                    laser.StartLaserBullets(powerUpProperties);
                    break;
                case PowerUpType.WallDrop:
                    wallDrop.StartWallDrop(powerUpProperties, ballList);
                    break;
            }
        }

        public void TryToResetInitialState()
        {
            extendShrinkPaddle.TryToResetAllExtensions();
            invisiblePaddle.TryToStopInvisiblePaddle();
            stopPaddleMovement.TryToStopPaddleMovementCoroutine();
            laser.TryToStopLaserBulletsCoroutine();
            wallDrop.TryToStopWallDropCoroutine();
        }

        public bool isStopPaddlePowerOn()
        {
            return stopPaddleMovement.IsStopPaddlePowerOn();
        }

        // public void ResetLaserSpawnPositions()
        // {
        //     extendShrinkPaddle.ResetLaserSpawnPositions();
        // }
        //
        // public void ResetWallDropSpawnPositions()
        // {
        //     extendShrinkPaddle.ResetWallDropSpawnPositions();
        // }
        public void ResetLaserAndWallDropSpawnPositions()
        {
            extendShrinkPaddle.ResetLaserAndWallDropSpawnPositions();
        }
    }
}