using System;
using System.Collections.Generic;
using BlockBreaker.Balls;
using BlockBreaker.Manager;
using BlockBreaker.PowerUps;
using BlockBreaker.Gameplay;
using UnityEngine;

namespace BlockBreaker.Paddles
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private PaddleProperties paddleProperties;
        [SerializeField] private Rigidbody2D myRigidbody2D;
        [SerializeField] private LaserBullet laserBulletPrefab;
        [SerializeField] private Transform paddleModelTransform, laserBulletLeftSpawn, laserBulletRightSpawn;
        [SerializeField] private GameObject wallDropLeftSpawn, wallDropRightSpawn;

        private float moveDirection;
        private Camera mainCamera;
        private Vector3 bottomLeftBorder, topRightBorder;
        private PaddlePowerUpController paddlePowerUpController;

        // FOR TEST, REMOVE IT AFTER NEW CHANGES
        private AutoPlay autoPlay;

        public void Initialize()
        {
            mainCamera = Camera.main;
            paddlePowerUpController = new PaddlePowerUpController();
            paddlePowerUpController.Initialize(
                this, laserBulletPrefab, laserBulletLeftSpawn, laserBulletRightSpawn, paddleModelTransform, paddleProperties, wallDropLeftSpawn, wallDropRightSpawn);
            ResetToInitialState();
            UpdateScreenBorders();
            // FOR TEST, REMOVE IT AFTER NEW CHANGES
            autoPlay = FindObjectOfType<AutoPlay>();
        }

        private void OnDestroy()
        {
            ResetToInitialState();
        }

        private void Update()
        {
            if (paddlePowerUpController.isStopPaddlePowerOn())
            {
                return;
            }

            if (!autoPlay.autoGamePlay)
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    MovePaddleWithTouch();
                }

                UpdateMovementDirection();
            }
        }

        private void FixedUpdate()
        {
            if (paddlePowerUpController.isStopPaddlePowerOn())
            {
                myRigidbody2D.velocity = Vector2.zero;
                return;
            }

            MovePaddleWithKeyboard();
        }

        private void MovePaddleWithTouch()
        {
            var originalX = transform.localScale.x;
            var touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var paddlePosition = transform.position;
            paddlePosition.x = Mathf.Clamp(touchPosition.x, bottomLeftBorder.x + paddleModelTransform.transform.localScale.x / 2 * (originalX / paddleProperties.paddleBaseSizeModelX),
                topRightBorder.x - paddleModelTransform.transform.localScale.x / 2 * (originalX / paddleProperties.paddleBaseSizeModelX));
            transform.position = new Vector3(paddlePosition.x, transform.position.y, transform.position.z);
        }

        private void MovePaddleWithKeyboard()
        {
            myRigidbody2D.velocity = new Vector2(moveDirection * paddleProperties.paddleMovementSpeed, 0f);
            var originalX = transform.localScale.x;
            var paddlePosition = transform.position;
            paddlePosition.x = Mathf.Clamp(paddlePosition.x, bottomLeftBorder.x + paddleModelTransform.transform.localScale.x / 2 * (originalX / paddleProperties.paddleBaseSizeModelX),
                topRightBorder.x - paddleModelTransform.transform.localScale.x / 2 * (originalX / paddleProperties.paddleBaseSizeModelX));
            transform.position = new Vector3(paddlePosition.x, transform.position.y, transform.position.z);
        }

        private void UpdateMovementDirection()
        {
            moveDirection = Input.GetAxisRaw("Horizontal");
        }

        private void UpdateScreenBorders()
        {
            bottomLeftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, mainCamera.transform.position.z));
            topRightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mainCamera.transform.position.z));
        }

        private void ResetToInitialState()
        {
            paddlePowerUpController.TryToResetInitialState();
            transform.localPosition = new Vector3(paddleProperties.paddleBasePositionX, paddleProperties.paddleBasePositionY, 0f);
            paddleModelTransform.transform.localScale = new Vector3(paddleProperties.paddleBaseSizeModelX, paddleProperties.paddleBaseSizeModelY, 0f);
            paddlePowerUpController.ResetLaserAndWallDropSpawnPositions();
        }

        public void ApplyPowerUp(PowerUpType powerUpType, PowerUpProperties powerUpProperties, List<Ball> ballList)
        {
            paddlePowerUpController.ApplyPowerUp(powerUpType, powerUpProperties, ballList);
        }

        // private void ResetLaserSpawnPositions()
        // {
        //     paddlePowerUpController.ResetLaserSpawnPositions();
        // }
    }
}