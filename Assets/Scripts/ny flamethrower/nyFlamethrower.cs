using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class nyFlamethrower : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 1f;  //how much damage the weapon does
        public float ShotsPerSecond { get; } = 30f; // how many shots per second the weapon can shot
        public float ProjectileSpeed { get; } = 20f;  //the speed of the fire

        float fireDamageTime;
        float currentShotsPerSecond;
        float shotTimer;

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

        // This method sets a damage cooldown on the player
        public bool DamageCooldown(PlayerScript hitPlayer, Collider flame)
        {
            hitPlayer = flame.GetComponent<PlayerScript>();

            if (hitPlayer != null && fireDamageTime < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // This method checks if a damage cooldown is active on the player or not and then deals damage according to the corresponding situation
        public void TargetHit(PlayerScript hitPlayer, Collider flame)
        {
            if (DamageCooldown(hitPlayer, flame))
            {
                hitPlayer.TakeDamage(0);
            }
            else
            {
                hitPlayer.TakeDamage(Damage);
                fireDamageTime = Time.deltaTime;
            }
        }
    }
}
