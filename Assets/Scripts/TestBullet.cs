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

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            parent = FindObjectOfType<TestGun>().gameObject;
            speed = parent.GetComponent<TestGun>().bulletSpeed;
            //speed = GetComponentInParent<TestGun>().bulletSpeed;
        }

        void Update()
        {
            rb.velocity = Vector3.left.normalized * speed;

        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(this.gameObject);
        }
    }
}
