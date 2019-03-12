using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 8f;

    bool lookingRight = true;

    public float MovDir { get; set; }

    private void FixedUpdate()
    {
        //rotate to match movement direction
        if(MovDir < 0 && lookingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            lookingRight = false;
        }
        else if(MovDir > 0 && !lookingRight)
        {
            transform.Rotate(new Vector3(0,-180, 0));
            lookingRight = true;
        }

        //apply movement
        transform.Translate(transform.right * -MovDir * Time.deltaTime * moveSpeed);
    }
}
