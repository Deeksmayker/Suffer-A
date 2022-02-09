using UnityEngine;

namespace Camera
{
    public class CameraTracking : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 10f;

        void FixedUpdate()
        {
            TrackPlayer();
        }

        private void TrackPlayer()
        {
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -100);
            
        }
    }
}