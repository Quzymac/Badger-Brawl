using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ProgressBar : MonoBehaviour
    {

        public Transform dtop;
        public Transform dbottom;
        public Transform dtarget;

        [SerializeField]
        private float camSpeed = 5f;
        float dyTop;
        float dyBottom;
        float dtotalDistance;
        float dpctValue;

        public float dpctDamage;


        private void Start()
        {
        }

        void FixedUpdate()
        {
            dyTop = dtop.position.y;
            dyBottom = dbottom.position.y;
            dtotalDistance = dyTop - dyBottom;

            if (dtarget.position != transform.position)
            {
                MoveCamera();
            }
        }
        private void MoveCamera()
        {
            Vector3 desiredPos = dtarget.position;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
            transform.position = smoothPos;
        }

        public void CalculatePosition() //Calcutes the position to which the camera should move to. Win condition needs to be here
        {
            if (dpctDamage > 100)
            {
                dpctDamage = 100;
                Debug.Log("Humans win");
            }
            if (dpctDamage < 0)
            {
                dpctDamage = 0;
                Debug.Log("Badgers win");
            }
            dpctValue = dtotalDistance * (dpctDamage / 100);
            Debug.Log(dpctValue);
            dtarget.position = new Vector3(dbottom.position.x, dyBottom + dpctValue, dbottom.position.z);
        }
        public void ChangeCameraPos(float value)
        {
            dpctDamage += value;
            CalculatePosition();
        }
    }
}
