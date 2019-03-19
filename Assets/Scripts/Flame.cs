using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Flame : MonoBehaviour
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

            if (distTraveled > 5)
            {
                Destroy(gameObject);
            }
        }

        //eventuellt flytta till en spelarklass
        public void DamageImmunity()
        {
            if (damageCooldown && fireDamageTime >= 2)
            {
                damageCooldown = false;
            }
        }

        private void OnTriggerEnter(Collider other)  //code about what happens when the fire hits the opponent
        {
            PlayerController playerHit = other.GetComponent<PlayerController>();
             
            if (playerHit != null)
            {
                if (!damageCooldown)
                {
                    playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                    damageCooldown = true;
                    fireDamageTime = Time.deltaTime;
                }
                else
                {
                    DamageImmunity();
                }              
            }

            if (other.tag != "Weapon")
            {
                Destroy(gameObject);
            }
        }


    }
}