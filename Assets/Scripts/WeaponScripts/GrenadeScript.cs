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
            //if (Owner == null && Firing)
            //{
            //    Firing = false;
            //}
            //if (Firing && Time.time > 1 / ShotsPerSecond + timer)
            //{
            //   Fire();
            //    timer = Time.time;
            //}

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
                    explosion.GetComponent<ExplosionDamage>().Weapon = Parent;
                    Destroy(gameObject);
                    startExplosion = false;
                    //blowUpTimer = 5f;
                }              
            }
        }

        public void Fire()
        {
            //GameObject grenade = Instantiate(gameObject, Owner.transform.position, Owner.transform.rotation);
            //rb.velocity = transform.forward * (ProjectileSpeed + holdIncreaseThrow) + new Vector3(0, holdIncreaseThrow * 20, 0);
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
