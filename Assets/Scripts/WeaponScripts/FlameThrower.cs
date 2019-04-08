using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class FlameThrower : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 5f;  //how much damage the weapon does
        public float ShotsPerSecond { get; } = 20f; // how many shots per second the weapon can shot
        public float ProjectileSpeed { get; } = 20f;  //the speed of the fire
        public float Range { get; } = 8f;
        
        float timer = 0f;
        public bool Firing { get; set; } = false;

        
        private AudioSource shotSound;
        float soundTimer = 0.2f;
        [SerializeField]
        Transform firePoint;

        [SerializeField]
        GameObject flame;

        public GameObject Owner { get; set; }  //depends on which player picks up the flamethrower
        public void Start()
        {
            shotSound = GetComponent<AudioSource>();
        }
        public void Fire()
        {
       
            GameObject newFlame = Instantiate(flame, firePoint.position, firePoint.rotation);
            newFlame.GetComponent<Flame>().Parent = gameObject;

        }


        void Update()
        {
            if (Owner == null && Firing)
            {
                Firing = false;
            }
            if (Firing && Time.time > 1 / ShotsPerSecond + timer)
            {
                soundTimer -= Time.deltaTime;
                if (soundTimer <= 0)
                {
                    shotSound.Play();
                    soundTimer = 0.5f;
                }

                Fire();
                timer = Time.time;
            }
        }
    }
}