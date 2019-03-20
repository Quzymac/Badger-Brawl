using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GrenadeScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 20f;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 10f;
        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        float timer = 0f;


        void Start()
        {

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
           
        }
    }
}
