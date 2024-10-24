using System.Collections;
using BlockBreaker.Manager;
using BlockBreaker.Paddles;
using UnityEngine;


namespace BlockBreaker.PowerUps
{
    public class Laser
    {
        private Paddle paddle;
        private LaserBullet laserBulletPrefab;
        private Transform laserBulletLeftSpawn, laserBulletRightSpawn;
        private IEnumerator laserBulletCoroutine;

        public void Initialize(Paddle paddle, LaserBullet laserBulletPrefab, Transform laserBulletLeftSpawn, Transform laserBulletRightSpawn)
        {
            this.paddle = paddle;
            this.laserBulletPrefab = laserBulletPrefab;
            this.laserBulletLeftSpawn = laserBulletLeftSpawn;
            this.laserBulletRightSpawn = laserBulletRightSpawn;
        }

        private IEnumerator StartLaserBulletsCoroutine(PowerUpProperties powerUpProperties)
        {
            var elapsedTime = 0f;
            while (elapsedTime <= powerUpProperties.powerUpDuration)
            {
                InstantiateLaserBullets();
                yield return new WaitForSeconds(powerUpProperties.laserBulletPerSecond);
                elapsedTime += powerUpProperties.laserBulletPerSecond;
            }

            laserBulletCoroutine = null;
            yield return null;
        }

        private void InstantiateLaserBullets()
        {
            Object.Instantiate(laserBulletPrefab, laserBulletLeftSpawn.position, Quaternion.identity);
            Object.Instantiate(laserBulletPrefab, laserBulletRightSpawn.position, Quaternion.identity);
        }

        public void StartLaserBullets(PowerUpProperties powerUpProperties)
        {
            TryToStopLaserBulletsCoroutine();
            laserBulletCoroutine = StartLaserBulletsCoroutine(powerUpProperties);
            paddle.StartCoroutine(laserBulletCoroutine);
        }

        public void TryToStopLaserBulletsCoroutine()
        {
            if (laserBulletCoroutine != null)
            {
                paddle.StopCoroutine(laserBulletCoroutine);
                laserBulletCoroutine = null;
            }
        }
    }
}