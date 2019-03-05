using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;

    bool lookingRight = true;
    public bool GetLookingRight()
    {
        return lookingRight;
    }

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

        Vector3 moveHorizontal = transform.right * -xMov;

        //apply movement
        transform.Translate(moveHorizontal * Time.deltaTime * moveSpeed);
    }
}
