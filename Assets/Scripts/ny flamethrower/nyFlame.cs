using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class nyFlame : MonoBehaviour
    {
        float distTraveled;
        Vector3 lastPos;
        Vector3 currentPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }
        bool damageCooldown = false;
        float fireDamageTime;

        void Start()
        {
            lastPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled += Vector3.Distance(transform.position, lastPos);
            lastPos = transform.position;

            if (distTraveled > 8)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)  //code about what happens when the fire hits the opponent
        {
            PlayerController playerHit = other.GetComponent<PlayerController>();

            if (playerHit != null)
            {
                playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
            }

            if (other.tag != "Weapon")
            {
                Destroy(gameObject);
            }
        }


    }
}
