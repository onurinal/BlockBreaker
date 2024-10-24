using System.Collections.Generic;
using BlockBreaker.Blocks;
using BlockBreaker.PowerUps;
using BlockBreaker.Paddles;
using BlockBreaker.Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlockBreaker.Balls
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallProperties ballProperties;
        [SerializeField] private AudioSource myAudioSource;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private Transform ballModelTransform;


        // state
        private bool ballLaunched;
        private bool isFirstBall;

        private Paddle paddle;
        private BallPowerUpController ballPowerUpController;

        // FOR TEST, REMOVE IT AFTER CHANGES
        private AutoPlay autoPlay;

        public void Initialize(Paddle paddle, bool isFirstBall)
        {
            this.paddle = paddle;
            this.isFirstBall = isFirstBall;
            ballPowerUpController = new BallPowerUpController();
            ballPowerUpController.Initialize(this, ballModelTransform);

            // FOR TEST, REMOVE IT AFTER CHANGE
            autoPlay = FindObjectOfType<AutoPlay>();
        }

        private void OnDestroy()
        {
            ballPowerUpController.TryToStopBallExtensionCoroutines();
        }

        private void Update()
        {
            if (!isFirstBall)
            {
                return;
            }

            if (ballLaunched || autoPlay.autoGamePlay)
            {
                return;
            }

            LockBallToPaddle();
            LaunchBallOnClick();
        }

        private void LockBallToPaddle() // lock the ball to paddle if the game not started
        {
            var ballPosition = paddle.transform.position + ballProperties.ballToPaddleDistance;
            transform.position = ballPosition;
        }

        private void LaunchBallOnClick()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            myRigidbody.velocity = ballProperties.ballVelocity;
            ballLaunched = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            myAudioSource.Play();
            collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(ballProperties.damage);
        }

        private void OnCollisionExit2D()
        {
            if (Mathf.Abs(myRigidbody.velocity.y) < ballProperties.minimumVerticalMovement)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,
                    Mathf.Sign(myRigidbody.velocity.y) * Random.Range(ballProperties.minimumVerticalMovement, ballProperties.maximumVerticalMovement));
            }

            if (Mathf.Abs(myRigidbody.velocity.x) < ballProperties.minimumHorizontalMovement)
            {
                myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * Random.Range(ballProperties.minimumHorizontalMovement, ballProperties.maximumHorizontalMovement),
                    myRigidbody.velocity.y);
            }

            KeepTheBallConstantSpeed();
        }

        private void KeepTheBallConstantSpeed()
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * ballProperties.ballVelocity.magnitude; // to keep constant speed
        }

        public void ApplyPowerUp(PowerUpType powerUpType, PowerUpProperties powerUpProperties)
        {
            ballPowerUpController.ApplyPowerUp(powerUpType, powerUpProperties);
        }
    }
}