using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShotgunScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 1f; //damage per bullet
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 30f;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;
        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;    
        [SerializeField] GameObject pellet;
        float timer = 0f;
        float currentShotsPerSecond;

        [SerializeField] int pelletCount;
        List<GameObject> pellets;
        Rigidbody rb;
        [SerializeField] float minSpread;
        [SerializeField] float maxSpread;
        [SerializeField] GameObject muzzleFlash;
        float flashDestroy = 2f;
        //public float amountHit { get; set; } = 0;
        public int[] amountHit = new int[4];

        public void Fire()
        {
            shotSound.Play();
            flashDestroy -= Time.deltaTime;
            for (int i = 0; i < amountHit.Length; i++)
            {
                amountHit[i] = 0;
            }
            firePoint.SetParent(gameObject.transform);
            GameObject thisFlash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            thisFlash.transform.parent = firePoint;
            foreach (GameObject pel in pellets)
            {
                GameObject thisPellet = Instantiate(pellet, firePoint.position, firePoint.rotation);
                thisPellet.GetComponent<PelletScript>().Parent = gameObject;
                rb = thisPellet.GetComponent<Rigidbody>();
                rb.velocity = transform.forward * ProjectileSpeed + new Vector3(0, Random.Range(minSpread, maxSpread), 0);              
            }
        }

        void Start()
        {
            shotSound = GetComponent<AudioSource>();
            pellets = new List<GameObject>(pelletCount);
            for (int i = 0; i < pelletCount; i++)
            {
                pellets.Add(pellet);
            }
        }

        void Update()
        {
            
            if (Owner == null && Firing)
            {
                Firing = false;
            }
            if (Firing && Time.time > 1 / ShotsPerSecond + timer)
            {
                Fire();
                timer = Time.time;
            }
            if (flashDestroy <= 0) // förstör inte gameobjektet ordentligt, kan optimeras
            {
                Destroy(muzzleFlash);
                flashDestroy = 2f;
            }
        }

        float thisDamage;

        public void MultiplePlayerHit(PlayerScript hitPlayer) //hanterar skada ifall olika kulor skullle träffa olika spelare
        {
            thisDamage = Damage;
            for (int i = 0; i < amountHit[hitPlayer.playerNumber - 1]; i++)
            {
                thisDamage *= 0.5f;
            }
            hitPlayer.TakeDamage(thisDamage);
            Debug.Log(thisDamage);
        }
    }
}
