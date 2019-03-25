using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class FlameThrower : MonoBehaviour, IWeapon
    {
        public GameObject shotSound { get; set; }
        public float Damage { get; } = 1f;  //how much damage the weapon does
        public float ShotsPerSecond { get; } = 30f; // how many shots per second the weapon can shot
        public float ProjectileSpeed { get; } = 20f;  //the speed of the fire

        public PlayerScript[] playersHit = new PlayerScript[4];

        float currentShotsPerSecond;
        float shotTimer;
        bool damageCooldown = false;
        float fireDamageTime;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField]
        Transform firePoint;

        [SerializeField]
        GameObject flame;

        public GameObject Owner { get; set; }  //depends on which player picks up the flamethrower

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
                Fire();
                timer = Time.time;
            }
        }

        public void DamageImmunity()
        {
            if (damageCooldown && fireDamageTime >= 2)
            {
                damageCooldown = false;
            }
        }

        float thisDamage;

        public void DamagePlayer(PlayerScript playerHit) //Nya metoden för att hantera damage 
        {
            thisDamage = Damage;
            if (!damageCooldown)
            {
                playerHit.TakeDamage(thisDamage);
                damageCooldown = true;
                fireDamageTime = Time.time;
            }
            else
            {
                DamageImmunity();
            }
        }

    }
}