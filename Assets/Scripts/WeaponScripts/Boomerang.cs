using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Boomerang : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;
        public float SeakingStrenght { get { return seakingStrenght; } }
        [SerializeField] float seakingStrenght = 0.4f;
        float timer = 0f;

        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject boomer;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        void Start()
        {
            shotSound = GetComponent<AudioSource>();
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
        public void Fire()
        {
            shotSound.Play();
            GameObject clone = Instantiate(boomer, firePoint.position, firePoint.rotation);

            clone.GetComponent<Boomeranger>().Owner = gameObject;
            clone.GetComponent<Boomeranger>().Parent = gameObject;
        }
    }
}
