using UnityEngine;

namespace BlockBreaker.Manager
{
    public class SpaceManager : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector3 bottomLeftBorder, topRightBorder;
        [SerializeField] private Transform rightBorderCollider, leftBorderCollider;

        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                bottomLeftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
                topRightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));
            }

            UpdateBorderColliderPositions();
        }

        private void UpdateBorderColliderPositions()
        {
            var newRightBorderPosition = rightBorderCollider.position;
            newRightBorderPosition.x = topRightBorder.x + rightBorderCollider.transform.localScale.x / 2;
            rightBorderCollider.position = newRightBorderPosition;
            var newLeftBorderPosition = leftBorderCollider.position;
            newLeftBorderPosition.x = bottomLeftBorder.x - leftBorderCollider.transform.localScale.x / 2;
            leftBorderCollider.position = newLeftBorderPosition;
        }
    }
}