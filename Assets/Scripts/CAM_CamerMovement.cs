using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_CamerMovement : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public Transform target;

    [SerializeField]
    private float camSpeed = 5f;
    //float yPos;
    float yTop;
    float yBottom;
    float totalDistance;
    float pctValue;
    [SerializeField]
    private float dmgValue;


    private void Start()
    {
        yTop = top.position.y;
        yBottom = bottom.position.y;

        totalDistance = yTop - yBottom;
        
    }

    void FixedUpdate()
    {
        //pctValue = totalDistance * (dmgValue / 100);
        //Debug.Log(pctValue);
        //target.position = new Vector3(0, pctValue, -10);
        //target.position = new Vector3(0, yBottom + pctValue, -10);
        CalculatePosition();
        MoveCamera();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveCamera();
        }*/
        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            yPos = transform.position.y;

            yPos -= 3f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            

            yPos += 3f;
        }*/
       
    }
    private void MoveCamera()
    {

        Vector3 desiredPos = target.position;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
        transform.position = smoothPos;
        //yPos = transform.position.y;
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, yPos, transform.position.z), camSpeed * Time.deltaTime);
    }

    private void CalculatePosition()
    {
        if(dmgValue>100)
        {
            dmgValue = 100;
        }
        if (dmgValue < 0)
        {
            dmgValue = 0;
        }
        pctValue = totalDistance * (dmgValue / 100);
        Debug.Log(pctValue);
        target.position = new Vector3(0, yBottom + pctValue, -10);
    }
}
