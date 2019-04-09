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

        public Vector3 offset;
        private Vector3 velocity;

        public float smoothTime = 0.5f;
        private float camSpeed = 5f;
        public float minZoom = 40f;
        public float maxZoom = 10f;
        public float zoomLimiter = 40.75f;

        public bool cameraMoving = false;
        //public bool bounds;
        public Vector3 minCameraPos;
        public Vector3 maxCameraPos;

        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            if (targets.Count == 0)
            {
                return;
            }
            //CameraLocked();
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

            //CameraLocked(newPosition);
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                   Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                   Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

        }

        void Zoom()
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
            //GetGreatestDistance();
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


        //private void CameraLocked(Vector3 newPosition)
        //{
        //    transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        //        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
        //            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
        //            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

        //}
    }
}