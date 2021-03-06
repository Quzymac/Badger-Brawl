﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TargetGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; set; }
        [SerializeField] float ThisDamage;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 13f;
        public float KnockBackPower { get { return knockBackPower; } }

        float currentShotsPerSecond;
        float shotTimer;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;
        [SerializeField] float knockBackPower = 15f;

        [SerializeField] float seakingStrenght = 0.4f;
        public float SeakingStrenght { get { return seakingStrenght; } }

        [SerializeField] float maxTravelTime = 4f;
        public float MaxTravelTime { get { return maxTravelTime; } }

        float timer = 0f;
        public bool Firing { get; set; } = false;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject rocket;

        public void Start()
        {
            Damage = ThisDamage;
            shotSound = GetComponent<AudioSource>();
        }

        public GameObject Owner { get; set; }

        public void Fire()
        {
            GameObject newBullet = Instantiate(rocket, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<TargetMissle>().Parent = gameObject;
            shotSound.Play();
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
