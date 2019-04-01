using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RocketLauncher : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 20f;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 10f;
        float currentShotsPerSecond;
        float shotTimer;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject rocket;

        public GameObject Owner { get; set; }

        public void Start()
        {
            shotSound = GetComponent<AudioSource>();
        }
        public void Fire()
        {
            shotSound.Play();
            GameObject newBullet = Instantiate(rocket, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<RocketBullet>().Parent = gameObject;
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
