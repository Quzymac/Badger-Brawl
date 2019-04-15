using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [RequireComponent(typeof(Camera))]
    public class MultipleTargetCam : MonoBehaviour
    {
        CAM_CamerMovement cameraMovement;
        GameManager gameManager;

        public List<Transform> targets;

        [SerializeField] Vector3 minCameraPos;
        [SerializeField] Vector3 maxCameraPos;
        public Vector3 offset;
        private Vector3 velocity;

        [SerializeField] float smoothTime = 1f;
        private float camSpeed = 5f;
        [SerializeField] float minZoom = 60f;
        [SerializeField] float maxZoom = 30f;
        [SerializeField] float zoomLimiter = 20f;

        public bool cameraMoving = false;

        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (targets.Count == 0)
            {
                return;
            }
            Move();
            Zoom();
        }
        void FixedUpdate()
        {
            if (gameManager.GetTargetPosition() != transform.position)
            {
                cameraMoving = true;
            }
            else if (cameraMoving && gameManager.GetTargetPosition() == transform.position)
            {
                cameraMoving = false;
            }
        }
        void Move()
        {
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

            int camClampOffset = gameManager._camRoundPosition;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, ((60 - cam.fieldOfView) / -2.5f), ((60 - cam.fieldOfView) / +2.5f)),
            Mathf.Clamp(transform.position.y, ((60 - cam.fieldOfView) / -5) + camClampOffset * 6, ((60 - cam.fieldOfView) / +5) + camClampOffset * 6),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
        void Zoom()
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }
        float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }
            return Mathf.Max(bounds.size.x, bounds.size.y);
        }
        Vector3 GetCenterPoint()
        {
            if (targets.Count == 0)
            {
                cameraMovement.UpdateCamPos();
            }

            if (targets.Count == 1)
            {
                return targets[0].position;
            }

            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }
            return bounds.center;
        }
        public void UpdateCamPos()
        {
            Vector3 desiredPos = gameManager.GetTargetPosition();
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
            transform.position = smoothPos;
            cam.fieldOfView = 60f;
        }
    }
}