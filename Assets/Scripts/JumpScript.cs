using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerbody;
    Vector3 platformPosition;
    int colliderValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(10, 10);
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("Hey platorm");
            Physics.IgnoreLayerCollision(9, 10);
        }
    }
}
