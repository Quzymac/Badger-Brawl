using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ChainGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 1f;
        public float ShotsPerSecond { get; } = 8f;
        public float ProjectileSpeed { get; } = 20f;
        public bool Firing { get; set; } = false;

        [SerializeField] float buildUpTime = 0.2f;


        float currentShotsPerSecond;
        [SerializeField] float minShotsPerSeconds = 2f;
        float timer = 0f;
        float prevTimer = 0f;
        

        [SerializeField] Transform firePoint;
        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        public void Fire()
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<TestBullet>().Parent = gameObject;
        }

        void Update()
        {
            if(Owner == null && Firing)
            {
                Firing = false;
            }
            if (Firing && Time.time > 1 / currentShotsPerSecond + timer)
            {
                Fire();
                timer = Time.time;
                currentShotsPerSecond += (timer - prevTimer) * buildUpTime;
            }
            if (currentShotsPerSecond >= ShotsPerSecond)
            {
                currentShotsPerSecond = ShotsPerSecond;
            }
            if (Firing == false)
            {
                currentShotsPerSecond = minShotsPerSeconds;
                prevTimer = timer;
            }
        }
    }
}

