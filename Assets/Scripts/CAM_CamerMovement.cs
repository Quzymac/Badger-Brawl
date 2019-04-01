using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CAM_CamerMovement : MonoBehaviour
    {

        public Transform top;
        public Transform bottom;
        public Transform target;
        [SerializeField]
        private float camSpeed = 5f;
        float yTop;
        float yBottom;
        float totalDistance;
        float pctValue;
        public bool cameraMoving = false;

        [SerializeField] ProgressBar progs;
        WinnerScript winnerScript;

        public float pctDamage;

        bool lastRoundBadger = false;
        bool lastRoundHuman = false;


        private void Start()
        {
            winnerScript = FindObjectOfType<WinnerScript>().GetComponent<WinnerScript>();
            yTop = top.position.y;
            yBottom = bottom.position.y;
            totalDistance = yTop - yBottom;
        }

        void FixedUpdate()
        {
            //CalculatePosition();
            if (target.position != transform.position)
            {
                MoveCamera();
                cameraMoving = true;
            }
            else if(cameraMoving && target.position == transform.position)
            {
                cameraMoving = false;
            }
        }
        private void MoveCamera()
        {
            Vector3 desiredPos = target.position;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
            transform.position = smoothPos;
        }

        private void CalculatePosition() //Calcutes the position to which the camera should move to. Win condition needs to be here
        {
            if (pctDamage > 100)
            {
                pctDamage = 100;
                if (lastRoundHuman)
                {
                    winnerScript.humansWon();
                }
                lastRoundHuman = true;
            }
            if (pctDamage < 0)
            {
                pctDamage = 0;
                if (lastRoundBadger)
                {
                    winnerScript.badgersWon();
                }
                lastRoundBadger = true;
            }
            pctValue = totalDistance * (pctDamage / 100);
            Debug.Log(pctValue);
            target.position = new Vector3(0, yBottom + pctValue, -20);
            progs.dpctDamage = pctDamage;
            progs.CalculatePosition();
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
            pctDamage += value;
            CalculatePosition();
        }
    }
}
