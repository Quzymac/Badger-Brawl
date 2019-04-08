using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CAM_CamerMovement : MonoBehaviour
    {
        [SerializeField] Transform topBorder;
        [SerializeField] Transform botBorder;
        public Transform top;
        public Transform bottom;
        public Transform target;
        [SerializeField]
        private float camSpeed = 5f;
        float topPos;
        float botPos;
        float yTop;
        float yBottom;
        float totalDistance;
        float pctValue;
        public bool cameraMoving = false;

        [SerializeField] ProgressBar progs;
        WinnerScript winnerScript;

        public float positionProcent;

        bool lastRoundBadger = false;
        bool lastRoundHuman = false;

        float minPositionX = -10.0f; // left border
        float maxPositionX = 10.0f; //  right border

        //public bool bounds;
        //public Vector3 minCameraPos;
        //public Vector3 maxCameraPos;


        private void Start()
        {
            winnerScript = FindObjectOfType<WinnerScript>().GetComponent<WinnerScript>();
            yTop = top.position.y;
            yBottom = bottom.position.y;
            totalDistance = yTop - yBottom;
        }

        void FixedUpdate()
        {
            //if (bounds)
            //{
            //    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), 
            //        Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y), 
            //        Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            //}
            //    float camX = transform.position.x;
            //    camX  = Mathf.Clamp(transform.position.x, minPositionX, maxPositionX);
            //topPos = topBorder.position.y;
            //botPos = botBorder.position.y;
            //Vector3 filip = new Vector3(transform.position.x, (transform.position.y - topPos) + topPos, transform.position.z);
            //Vector3 filip2 = new Vector3(transform.position.x, (transform.position.y + botPos) - botPos, transform.position.z);

            ////////Vector3 filip2 = new Vector3(transform.position.x, (transform.position.y - botPos) + botPos, transform.position.z);

            //transform.position = filip2;
            //transform.position = filip;
            //transform.position.y = Mathf.Clamp(transform.position.y, botPos, topPos);
            //CalculatePosition();

            //CameraLock();
            if (target.position != transform.position)
            {
                //UpdateCamPos();
                cameraMoving = true;
            }
            else if(cameraMoving && target.position == transform.position)
            {
                cameraMoving = false;
            }
        }
        public void UpdateCamPos()
        {
            Vector3 zOffset = new Vector3(0, 0, -20);
            Vector3 desiredPos = target.position + zOffset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
            transform.position = smoothPos;
        }

        private void CalculatePosition() //Calcutes the position to which the camera should move to. Win condition needs to be here
        {
            if (positionProcent >= 100)
            {
                positionProcent = 100;
                if (lastRoundHuman)
                {
                    winnerScript.humansWon();
                    lastRoundHuman = false;
                }
                lastRoundHuman = true;
            }
            if (positionProcent <= 0)
            {
                positionProcent = 0;
                if (lastRoundBadger)
                {
                    winnerScript.badgersWon();
                    lastRoundBadger = false;
                }
                lastRoundBadger = true;
            }
            pctValue = totalDistance * (positionProcent / 100);
            Debug.Log(pctValue);
            target.position = new Vector3(0, yBottom + pctValue, -20);
            progs.dpctDamage = positionProcent;
            progs.CalculatePosition();
        }

        private void CameraLock()
        {
            topPos = topBorder.position.y;
            botPos = botBorder.position.y;
            if (transform.position.y == botPos)
            {
                //transform.position = botBorder;
                Debug.Log("working");
            }

            
            //Vector3 topLock = new Vector3(transform.position.x, (transform.position.y - topPos) + topPos, transform.position.z);
            //if (transform.position.y == topPos)
            //{
            //    transform.position = topLock;
            //}

            //Vector3 filip2 = new Vector3(transform.position.x, (transform.position.y - botPos) + botPos, transform.position.z);


        }
        public void ChangeCameraPos(float value)
        {
            if (value > 0 && lastRoundBadger)
            {
                lastRoundBadger = false;
            }
            if (value < 0 && lastRoundHuman)
            {
                lastRoundHuman = false;
            }
            positionProcent += value;
            CalculatePosition();
        }
    }
}
