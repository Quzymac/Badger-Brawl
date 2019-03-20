using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShotgunScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 0.6f; //damage per bullet
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 30f;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        [SerializeField] Transform firePoint;
        [SerializeField] GameObject pellet;
        float timer = 0f;
        float currentShotsPerSecond;

        [SerializeField] int pelletCount;
        List<GameObject> pellets;
        Rigidbody rb;
        [SerializeField] float minSpread;
        [SerializeField] float maxSpread;

        public void Fire()
        {
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
        }
    }
}
