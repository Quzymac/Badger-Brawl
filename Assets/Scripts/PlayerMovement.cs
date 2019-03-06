using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;

    bool lookingRight = true;

    private void FixedUpdate()
    {
        
         //calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");

        //rotate to match movement direction
        if(xMov < 0 && lookingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            lookingRight = false;
        }
        else if(xMov > 0 && !lookingRight)
        {
            transform.Rotate(new Vector3(0,-180, 0));
            lookingRight = true;
        }

        //apply movement
        transform.Translate(transform.right * -xMov * Time.deltaTime * moveSpeed);
    }
}
