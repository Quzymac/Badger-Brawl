using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GrenadeScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 20f;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 25f;
        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        float timer = 0f;
        public GameObject Parent { get; set; }

        float holdIncreaseThrow = 0f;
        float getHoldValue = 0f;
        float maxHoldThrow = 3f;

        [SerializeField] private AudioSource shotSound;
        Rigidbody rb;
        bool isThrown = false;
        bool startExplosion = false;
        [SerializeField] GameObject Explosion;
        float blowUpTimer = 5f;


        void Start()
        {
            rb = GetComponent<Rigidbody>();          
        }

        void Update()
        {
            if (Firing == true)
            {
                holdIncreaseThrow += Time.deltaTime;
                if (holdIncreaseThrow >= maxHoldThrow)
                {
                    Instantiate(Explosion, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
            else if (Firing == false && holdIncreaseThrow > 1)
            {
                getHoldValue = holdIncreaseThrow;
                holdIncreaseThrow = 0;
                isThrown = true;
            }
            else if (Firing == false && holdIncreaseThrow >= 5)
            {
                getHoldValue = holdIncreaseThrow;
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (isThrown == true)
            {
                isThrown = false;
                GetComponent<Collider>().enabled = true;
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                transform.parent = null;
                GetComponent<IWeapon>().Owner = null;
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
                    Destroy(gameObject);
                    startExplosion = false;
                    //blowUpTimer = 5f;
                }              
            }
        }

        public void Fire()
        {
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

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (startExplosion == true)
        //    {
        //        if (collision.collider.tag == "Player")
        //        {
        //            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
        //        }
        //    }

        //}
    }
}
