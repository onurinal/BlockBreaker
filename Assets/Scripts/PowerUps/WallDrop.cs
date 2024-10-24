using System.Collections;
using System.Collections.Generic;
using BlockBreaker.Balls;
using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;

namespace BlockBreaker.PowerUps
{
    public class WallDrop
    {
        private Paddle paddle;
        private GameObject wallDropLeftSpawn;
        private GameObject wallDropRightSpawn;
        private IEnumerator wallDropRoutine;

        public void Initialize(Paddle paddle, GameObject wallDropLeftSpawn, GameObject wallDropRightSpawn)
        {
            this.paddle = paddle;
            this.wallDropLeftSpawn = wallDropLeftSpawn;
            this.wallDropRightSpawn = wallDropRightSpawn;
        }

        private IEnumerator WallDropPowerUp(PowerUpProperties powerUpProperties, List<Ball> ballList)
        {
            wallDropLeftSpawn.SetActive(true);
            wallDropRightSpawn.SetActive(true);
            for (var i = 0; i <= ballList.Count - 1; i++)
            {
                var ballPosition = ballList[i].transform.position;
                ballPosition.x = Random.Range(wallDropLeftSpawn.transform.position.x / 1.2f, wallDropRightSpawn.transform.position.x / 1.4f);
                ballPosition.y = Random.Range(ballList[i].transform.position.y / 4f, ballList[i].transform.position.y);
                ballList[i].transform.position = ballPosition;
            }

            yield return new WaitForSeconds(powerUpProperties.powerUpDuration);
            wallDropRightSpawn.SetActive(false);
            wallDropLeftSpawn.SetActive(false);
            wallDropRoutine = null;
        }

        public void StartWallDrop(PowerUpProperties powerUpProperties, List<Ball> ballList)
        {
            TryToStopWallDropCoroutine();
            wallDropRoutine = WallDropPowerUp(powerUpProperties, ballList);
            paddle.StartCoroutine(wallDropRoutine);
        }

        public void TryToStopWallDropCoroutine()
        {
            if (wallDropRoutine != null)
            {
                paddle.StopCoroutine(wallDropRoutine);
                wallDropRoutine = null;
            }
        }
    }
}