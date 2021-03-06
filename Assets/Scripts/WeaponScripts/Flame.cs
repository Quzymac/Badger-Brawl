﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Flame : MonoBehaviour
    {
        float distTraveled;
        Vector3 startPos;
        Vector3 currentPos;
        Rigidbody rb;

        float range;
        public GameObject Parent { get; set; }

        [SerializeField] BoxCollider col;
        PlayerScript.PlayerTeam team;

        void Start()
        {
            team = Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team;
            range = Parent.GetComponent<FlameThrower>().Range;
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);

            if (distTraveled > range)
            {
                Destroy(gameObject);
            }
            col.size = new Vector3(1f, distTraveled * 0.25f, 0.25f);
        }

        private void OnTriggerEnter(Collider other)  //code about what happens when the fire hits the opponent
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();

            if (playerHit != null)
            {
                if (playerHit.Team != team)
                {
                    playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage * 0.1f);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "NonDestroy")
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
            }
        }
    }
}
