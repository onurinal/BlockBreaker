using UnityEngine;

namespace BlockBreaker.Balls
{
    [CreateAssetMenu(menuName = "BlockBreaker/Ball/Ball Properties")]
    public class BallProperties : ScriptableObject
    {
        [Header("Ball Movement")] [SerializeField]
        public Vector2 ballVelocity = new Vector2(0f, 8f);

        public int damage = 1;

        [Header("Ball To Paddle Distance")] [SerializeField]
        public Vector3 ballToPaddleDistance = new Vector3(0f, 0.2f, 0f);

        public float minimumVerticalMovement = 1f;
        public float maximumVerticalMovement = 3f;
        public float minimumHorizontalMovement = 1f;
        public float maximumHorizontalMovement = 3f;
    }
}