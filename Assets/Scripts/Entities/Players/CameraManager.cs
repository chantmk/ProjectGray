using UnityEngine;

namespace Players
{
    public class CameraManager : MonoBehaviour
    {
        private Transform target;
        public float SmoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;
        [SerializeField] private GameObject CameraHolder;

        void Start()
        {
            target = GameObject.Find("Player").transform;
        }

        void Update()
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            if (target.position.x > 13)
            {
                targetPosition = new Vector3(12, 4.3f, -10);
            }

            // Smoothly move the camera towards that target position
            CameraHolder.transform.position = Vector3.SmoothDamp(CameraHolder.transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
}