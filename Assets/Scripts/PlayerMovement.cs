using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;

    bool lookingLeft = true;
    

    private void FixedUpdate()
    {

         //calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        //float xMov = Input.GetAxis("Horizontal");


        //if (xMov < 0.1)
        //{
        //    transform.rotation.eulerAngles =  Vector3(0, 90, 0);
        //}

        Vector3 moveHorizontal = transform.right * xMov;

        //apply movement
        transform.Translate(moveHorizontal * Time.deltaTime * moveSpeed);
    }
}
