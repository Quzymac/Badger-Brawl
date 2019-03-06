using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TestBullet : MonoBehaviour
    {
        Rigidbody rb;
        float speed;
        float damage;
        GameObject parent;
        Vector3 dir;
       

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            parent = FindObjectOfType<TestGun>().gameObject;
            speed = parent.GetComponent<TestGun>().BulletSpeed;
            damage = parent.GetComponent<TestGun>().Damage;

            rb.velocity = transform.forward * speed;
        }
        

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerHit = other.GetComponent<PlayerController>();
            if(playerHit != null)
            {
                playerHit.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
