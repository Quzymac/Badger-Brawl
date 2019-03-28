using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class FlameThrower : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 1f;  //how much damage the weapon does
        public float ShotsPerSecond { get; } = 30f; // how many shots per second the weapon can shot
        public float ProjectileSpeed { get; } = 20f;  //the speed of the fire
        public GameObject fireSpace;
        
        float currentShotsPerSecond;
        float shotTimer;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField]
        private GameObject shotSound;
        [SerializeField]
        Transform firePoint;

        [SerializeField]
        GameObject flame;

        public GameObject Owner { get; set; }  //depends on which player picks up the flamethrower

        public void Fire()
        {
            GameObject gunShot = Instantiate(shotSound, this.transform.position, this.transform.rotation) as GameObject;
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

        float fireDamageTime;

        //This method sets a damage cooldown on the player

        public bool DamageCooldown(PlayerScript playerHit)
        {
            playerHit = GetComponent<PlayerScript>();

            if (playerHit  != null && fireDamageTime >= 0 )
            {
                return true;  // DamageCoolDown is true
            }
            else
            {
                return false;  //DamageCoolDown is false
            }
        }

         //This method checks if a damage cooldown is active on the player or not and then deals damage according to the corresponding situation
        public void TargetHit(PlayerScript playerHit)
        {

            if (DamageCooldown(playerHit))  //if DamageCoolDown is true, this code will start, the target wont take any damage
            {
                playerHit.TakeDamage(0);
                Debug.Log("no damage");
            }
            else  // if DamageCoolDown is false this code will start, the target will take damage and the timer will be set to 5 again and then start countiong down
            {
                playerHit.TakeDamage(Damage);
                Debug.Log("aj");
            }
        }
    }
}