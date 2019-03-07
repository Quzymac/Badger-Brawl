using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerbody;
    Collider player;

    [SerializeField]
    GameObject collidedPlatform;

    void Start()
    {
        playerbody = GetComponent<Rigidbody>();
        player = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            gameObject.layer = 12;
            Debug.Log(collidedPlatform);      
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (collidedPlatform.transform.position.y <= gameObject.transform.position.y)
            {
                Debug.Log("Got Here!!!!");
                Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), player, true);

                
            }
        }

        if (collidedPlatform.transform.position.y >= gameObject.transform.position.y)
        {
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), player, false);
        }

        if (playerbody.velocity.y <= 0 && gameObject.layer == 12)
        {
            gameObject.layer = 9;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidedPlatform = collision.gameObject;      
    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
