﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RocketBullet : MonoBehaviour
    {
        float distTraveled;
        Vector3 startPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }
        public float Damage { get; set; }

        [SerializeField] GameObject Explosion; // Tillfällig explosionseffekt
        PlayerScript.PlayerTeam team;

        void Start()
        {
            team = Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team;
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);

            if (distTraveled > 20)
            {
                GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<RocketLauncher>().Damage;

                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                if (playerHit.Team != team)
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<RocketLauncher>().Damage;

                    Destroy(gameObject);
                }
            }
            else {

                if (other.tag != "Weapon")
                {
                GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<RocketLauncher>().Damage;

                Destroy(gameObject);

                }
            }
            
        }
    }
}
