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

        public float pctDamage;


        private void Start()
        {
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
                Debug.Log("Humans win");
            }
            if (pctDamage < 0)
            {
                pctDamage = 0;
                Debug.Log("Badgers win");
            }
            pctValue = totalDistance * (pctDamage / 100);
            Debug.Log(pctValue);
            target.position = new Vector3(0, yBottom + pctValue, -20);
        }
        public void ChangeCameraPos(float value)
        {
            pctDamage += value;
            CalculatePosition();
            //MoveCamera();
        }
  
    }
}
