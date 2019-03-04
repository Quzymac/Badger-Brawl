using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
   

    private void FixedUpdate()
    {
         //calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");

        Vector3 moveHorizontal = transform.right * xMov;

        //apply movement
        transform.Translate(moveHorizontal * Time.deltaTime * moveSpeed);
    }
}
