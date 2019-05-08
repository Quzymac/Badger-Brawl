﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GrenadeScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 30f;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 25f;
        public bool Firing { get; set; } = false;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 20f;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.throwable;
        public GameObject Owner { get; set; }
        float timer = 0f;
        public GameObject Parent { get; set; }

        float holdIncreaseThrow = 0.5f;
        float getHoldValue = 0f;
        float maxHoldThrow = 3f;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        private AudioSource shotSound;
        Rigidbody rb;
        bool isThrown = false;
        bool startExplosion = false;
        [SerializeField] GameObject Explosion;
        float blowUpTimer = 5f;

        

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            shotSound = GetComponent<AudioSource>();
            shotSound.Stop();
        }

        void Update()
        {
            if (Firing == true)
            {
                holdIncreaseThrow += Time.deltaTime;
                if (holdIncreaseThrow >= maxHoldThrow)
                {
                    Instantiate(Explosion, transform.position, transform.rotation);
                    GetComponent<WeaponDespawn>().Despawn(); //destroy and remove from list of weapons
                }
            }
            else if (Firing == false && holdIncreaseThrow > 0.5f)
            {
                getHoldValue = holdIncreaseThrow;
                holdIncreaseThrow = 0;
                isThrown = true;
            }
            else if (Firing == false && holdIncreaseThrow >= 5)
            {
                getHoldValue = holdIncreaseThrow;
                Instantiate(Explosion, transform.position, transform.rotation);
                GetComponent<WeaponDespawn>().Despawn(); //destroy and remove from list of weapons
            }

            if (isThrown == true)
            {
                isThrown = false;
                GetComponent<Collider>().enabled = true;
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                transform.parent = null;
                Owner.GetComponent<IKHandler>().RightHand = null;
                Owner.GetComponent<IKHandler>().LeftHand = null;
                Owner = null;

                Fire();
                startExplosion = true;
            }

            if (startExplosion == true)
            {
                blowUpTimer -= Time.deltaTime;
                Debug.Log(blowUpTimer);
                if (blowUpTimer <= 0)
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.GetComponent<ExplosionDamage>().Damage = Damage;
                    explosion.GetComponent<ExplosionDamage>().KnockBackPower = KnockBackPower;

                    GetComponent<WeaponDespawn>().Despawn(); //destroy and remove from list of weapons
                    startExplosion = false;
                }              
            }
        }

        public void Fire()
        {
            shotSound.Play();

            blowUpTimer = 5f;
            rb.AddForce(transform.forward * (ProjectileSpeed * (getHoldValue * 20)) + new Vector3(0, getHoldValue * 100, 0));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (startExplosion == true)
            {
                if (other.tag == "Platform")
                {
                    blowUpTimer *= 0.5f;
                }
            }
        }
    }
}
