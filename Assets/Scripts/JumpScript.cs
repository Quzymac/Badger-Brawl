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

    //Vector3 platformPosition;
    int colliderValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody>();
        player = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.layer = 12;
            Debug.Log(collidedPlatform);
            //Physics.IgnoreLayerCollision(9, 10);          
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), player, true);
            Debug.Log("Hey platorm");
        }
        else if (collidedPlatform.transform.position.y <= player.transform.position.y)
        {
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), player, false);
            //Debug.Log("player higher now");
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
