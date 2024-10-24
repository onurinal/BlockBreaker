using UnityEngine;

namespace BlockBreaker.PowerUps
{
    [CreateAssetMenu(menuName = "BlockBreaker/Gameplay/PowerUp Properties")]
    public class PowerUpProperties : ScriptableObject
    {
        [Header("General PowerUp Properties")] public float powerUpDuration = 3f;

        [Header("MultiBall Properties")] public int maxBallCount = 9;
        public float multiBallYSpeed = 8f;

        [Header("Paddle Extend/Shrink Properties")]
        public float paddleBaseSizeModelX = 0.15f;

        public float minPaddleExtendShrinkAmount = -0.05f;
        public float maxPaddleExtendShrinkAmount = 0.15f;
        public float minPaddleSizeModelX = 0.1f;
        public float maxPaddleSizeModelX = 0.3f;
        public float perExtendPaddleSizeModelX = 0.075f;
        public float perShrinkPaddleSizeModelX = -0.05f;
        public float paddleScaleUpDuration = 0.2f;

        [Header("Ball Extend/Shrink Properties")]
        public float ballBaseSizeModelX = 0.15f;

        public float ballBaseSizeModelY = 0.15f;
        public float minBallExtendShrinkAmount = -0.05f;
        public float maxBallExtendShrinkAmount = 0.30f;
        public float minBallSizeModelX = 0.1f;
        public float maxBallSizeModelX = 0.45f;
        public float perExtendBallSizeModelX = 0.15f;
        public float perShrinkBallSizeModelX = -0.05f;
        public float ballScaleUpDuration = 0.3f;

        [Header("Laser PowerUp Properties")] public int laserBulletDamage = 1;
        public float laserBulletSpeedY = 8f;
        public float laserBulletPerSecond = 0.3f;
    }
}