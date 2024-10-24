using System.Collections.Generic;
using BlockBreaker.Blocks;
using BlockBreaker.Balls;
using BlockBreaker.Paddles;
using BlockBreaker.PowerUps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlockBreaker.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Paddle paddlePrefab;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private PaddleProperties paddleProperties;
        [SerializeField] private BallProperties ballProperties;
        [SerializeField] public int maxPlayerLife;

        private List<Ball> ballList = new List<Ball>();
        public List<BreakableBlock> breakableBlockList = new List<BreakableBlock>();

        private Vector3 paddleSpawnPosition;
        private int currentPlayerLife;
        private bool isGameOver;

        private Ball ball;
        private Paddle paddle;

        public static GameManager Instance; // singleton

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            Application.targetFrameRate = 60;
            ResetGame();
        }

        private void ResetGame()
        {
            paddleSpawnPosition = new Vector3(paddleProperties.paddleBasePositionX, paddleProperties.paddleBasePositionY, 0f);
            currentPlayerLife = maxPlayerLife;
            CreateNewPaddle();
            CreateNewBall();
        }

        // -------------MANAGE BALL AND PADDLE-------------
        private void CreateNewPaddle()
        {
            paddle = Instantiate(paddlePrefab, paddleSpawnPosition, Quaternion.identity);
            paddle.Initialize();
        }

        private void CreateNewBall()
        {
            ball = Instantiate(ballPrefab);
            ballList.Add(ball);
            ball.Initialize(paddle, true);
        }

        public void RemoveBall(Ball ball)
        {
            ballList.Remove(ball);
            Destroy(ball.gameObject);
            if (ballList.Count <= 0 && !isGameOver)
            {
                RemoveLife();
            }
        }

        public void RemoveLife()
        {
            currentPlayerLife--;
            UIManager.Instance.RemoveLifeIcon(currentPlayerLife);
            if (currentPlayerLife <= 0 && !isGameOver)
            {
                // Game Over Condition
                isGameOver = true;
                DestroyPaddle();
                SceneLoader.Instance.LoadGameOver();
                return;
            }

            DestroyAllBall();
            ResetPaddle();
            CreateNewBall();
        }

        private void DestroyAllBall()
        {
            if (ballList.Count > 0)
            {
                for (var i = ballList.Count - 1; i >= 0; i--)
                {
                    Destroy(ballList[i].gameObject);
                }

                ballList.Clear();
            }
        }

        private void ResetPaddle()
        {
            Destroy(paddle.gameObject);
            CreateNewPaddle();
        }

        private void DestroyPaddle()
        {
            Destroy(paddle.gameObject);
        }

        // -------------MANAGE BLOCKS-------------
        public void CountBreakableBlock(BreakableBlock block)
        {
            breakableBlockList.Add(block);
        }

        public void RemoveBreakableBlock(BreakableBlock block)
        {
            breakableBlockList.Remove(block);
            if (breakableBlockList.Count > 0)
            {
                return;
            }

            // WIN CONDITION
            DestroyAllBall();
            DestroyPaddle();
            UIManager.Instance.winScreenPanel.gameObject.SetActive(true);
        }

        // ------------- MANAGE POWER UPS-------------
        private void CreateMultiBall(PowerUpProperties powerUpProperties)
        {
            for (var i = ballList.Count - 1; i >= 0; i--)
            {
                if (ballList.Count >= powerUpProperties.maxBallCount)
                {
                    return;
                }

                var ballPosition = ballList[i].transform.position;
                for (var j = 0; j < 2; j++)
                {
                    var newBall = Instantiate(ballList[0], ballPosition, Quaternion.identity);
                    newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4, 4), powerUpProperties.multiBallYSpeed);
                    ballList.Add(newBall);
                    newBall.Initialize(paddle, false);
                }
            }
        }

        public void PowerUpCollected(PowerUpType powerUpType, PowerUpProperties powerUpProperties)
        {
            switch (powerUpType)
            {
                case PowerUpType.ExtendPaddleSize:
                case PowerUpType.ShrinkPaddleSize:
                case PowerUpType.StopPaddleMovement:
                case PowerUpType.InvisiblePaddle:
                case PowerUpType.Laser:
                case PowerUpType.WallDrop:
                    paddle.ApplyPowerUp(powerUpType, powerUpProperties, ballList);
                    break;
                case PowerUpType.MultiBall:
                    CreateMultiBall(powerUpProperties);
                    break;
                case PowerUpType.ExtendBallSize:
                case PowerUpType.ShrinkBallSize:
                    for (var i = 0; i <= ballList.Count - 1; i++)
                    {
                        ballList[i].ApplyPowerUp(powerUpType, powerUpProperties);
                    }

                    break;
            }
        }
    }
}