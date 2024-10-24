using UnityEngine;

namespace BlockBreaker.Paddles
{
    [CreateAssetMenu(menuName = "BlockBreaker/Paddle/Paddle Properties")]
    public class PaddleProperties : ScriptableObject
    {
        public float paddleMovementSpeed = 5f;

        [Header("Paddle Base Position")] [SerializeField]
        public float paddleBasePositionX = 0f;

        public float paddleBasePositionY = -5f;

        [Header("Paddle Base Size")] [SerializeField]
        public float paddleBaseSizeModelX = 0.15f;

        public float paddleBaseSizeModelY = 0.15f;

        [Header("Paddle Base Laser Spawn Positions")]
        public float paddleLaserLeftSpawnBasePositionX = -0.375f;

        public float paddleLaserRightSpawnBasePositionX = 0.375f;
        public float paddleLaserSpawnPositionModifier = 2.5f;

        [Header("Paddle Base WallDrop Spawn Positions")]
        public float paddleWallDropLeftSpawnBasePositionX = -0.39f;

        public float paddleWallDropRightSpawnBasePositionX = 0.39f;
        public float paddleWallDropSpawnPositionModifier = 2.6f;
    }
}