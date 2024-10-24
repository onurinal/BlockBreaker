using UnityEngine;
using BlockBreaker.Balls;
using BlockBreaker.Paddles;

namespace BlockBreaker.Gameplay
{
    public class AutoPlay : MonoBehaviour
    {
        [SerializeField] public bool autoGamePlay;

        [Range(0.1f, 50f)] [SerializeField] private float gameSpeed;

        private Ball ball;
        private Paddle paddle;

        private void Start()
        {
            ball = FindObjectOfType<Ball>();
            paddle = FindObjectOfType<Paddle>();
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(4f, 13f);
        }

        private void Update()
        {
            if (autoGamePlay)
            {
                var paddlePosition = paddle.transform.position;
                paddlePosition.x = Mathf.Clamp(ball.transform.position.x + 0.3f, -2.3f, 2.3f);
                paddle.transform.position = paddlePosition;
            }

            Time.timeScale = gameSpeed;
        }
    }
}