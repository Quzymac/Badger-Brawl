using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    Rigidbody rb;
    Collider playerCollider;

    GameObject collidedPlatform;

    [SerializeField] float jumpForce = 700f;
    [SerializeField] float gravity = 16f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] bool onGround = true;
    [SerializeField] bool canDoubleJump = false;
    [SerializeField] LayerMask platformLayer;
    Vector3 jumpDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();

        jumpDir = new Vector3(0, jumpForce, 0);
        Physics.gravity = new Vector3(0, -gravity, 0);
    }

    public void Jump()
    {

        if (onGround)
        {
            //jumpCount++;
            canDoubleJump = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(jumpDir);
            gameObject.layer = 12;
        }

        else if (!onGround && canDoubleJump)
        {
            canDoubleJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(jumpDir);
            gameObject.layer = 12;
        }
    }

    public void DropThrough()
    {
        if (collidedPlatform!= null && collidedPlatform.transform.position.y <= gameObject.transform.position.y)
        {
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, true);
        }
    }
    private void FixedUpdate()
    {
        //better jump, hålla in "hopp" för längre hopp
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow)) //testing, change button later------------------------------------------------------------
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Update()
    {
        GroundDetection();

        if (rb.velocity.y < 0 && gameObject.layer == 12 && collidedPlatform != null)
        {            
            gameObject.layer = 10;
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
        }
        else if (rb.velocity.y < 0 && gameObject.layer == 10)
        {
            if (collidedPlatform != null && collidedPlatform.transform.position.y >= gameObject.transform.position.y)
            {
                Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
            }
        }
        else if (rb.velocity.y > 0 && collidedPlatform != null)
        {
            Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
        }
    }

    private void GroundDetection()
    {
        RaycastHit hit;
        Vector2 physicsCentre = transform.position + this.GetComponent<SphereCollider>().center;
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red, 1);

        if (Physics.Raycast(physicsCentre, Vector2.down, out hit, 0.6f, platformLayer))
        {
            //canDoubleJump = false;
            onGround = true;
        }
        else
        {
            if (Physics.Raycast(transform.position, new Vector2(-1, -1), out hit, 0.7f, platformLayer) || Physics.Raycast(transform.position, new Vector2(1, -1), out hit, 0.7f, platformLayer))
            {
                Debug.DrawRay(transform.position, new Vector2(-1, -1) * 0.7f, Color.blue, 1);
                Debug.DrawRay(transform.position, new Vector2(1, -1) * 0.7f, Color.green, 1);

                //canDoubleJump = false;
                onGround = true;
            }
            else
                onGround = false;
        }
        Debug.Log(onGround);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidedPlatform = collision.gameObject;      
    }
}
