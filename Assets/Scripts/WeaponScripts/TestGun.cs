﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{

    public class TestGun : MonoBehaviour, IWeapon

    {
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 2f;
        public float ProjectileSpeed { get; } = 20f;

        float currentShotsPerSecond;
        float shotTimer;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        public void Start()
        {
            shotSound = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            shotSound.Play();
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<TestBullet>().Parent = gameObject;
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
