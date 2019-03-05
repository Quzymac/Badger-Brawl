using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TestBullet : MonoBehaviour
    {
        Rigidbody rb;
        float speed;
        GameObject parent;
        Vector3 dir;
       

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            parent = FindObjectOfType<TestGun>().gameObject;
            speed = parent.GetComponent<TestGun>().bulletSpeed;

            dir = new Vector3(-1, 0, 0);

            Debug.Log(parent.transform.localToWorldMatrix.rotation);


            //if (parent.GetComponent<TestGun>().Owner.GetComponent<PlayerMovement>().GetLookingRight())
            //{
            //    dir = new Vector3(1,0,0);
            //}
            //else
            //{
            //    dir = new Vector3(-1, 0, 0);
            //}
        }

        void Update()
        {

            rb.velocity = dir * speed; // (parent.transform.rotation.normalized.eulerAngles) * speed;  //Vector3.left.normalized * speed;

        }

        private void OnTriggerEnter(Collider other)
        {
            //if (!other.CompareTag("Player") || !other.CompareTag("Weapon"))
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}
