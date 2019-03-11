﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TestBullet : MonoBehaviour
    {
        Rigidbody rb;
        public GameObject Parent { get; set; }

        void Start()
        {
            Debug.Log(Parent.GetComponent<TestGun>().Owner);
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<TestGun>().Owner.GetComponent<Collider>(), true);
        }


        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerHit = other.GetComponent<PlayerController>();
            if(playerHit != null)
            {
                playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
            }

            Destroy(gameObject);
        }
    }
}
