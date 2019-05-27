using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class FlameThrower : MonoBehaviour, IWeapon
    {
        public float Damage { get; set; }  //how much damage the weapon does
        [SerializeField] float ThisDamage;
        public float ShotsPerSecond { get; } = 20f; // how many shots per second the weapon can shot
        public float ProjectileSpeed { get; } = 20f;  //the speed of the fire
        public float Range { get; } = 8f;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 0.1f;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        private AudioSource shotSound;
        float soundTimer = 0;
        [SerializeField]
        Transform firePoint;

        [SerializeField]
        GameObject flame;    

        public GameObject Owner { get; set; }  //depends on which player picks up the flamethrower
        public void Start()
        {
            Damage = ThisDamage;
            shotSound = GetComponent<AudioSource>();
        }
        public void Fire()
        {   
            GameObject newFlame = Instantiate(flame, firePoint.position, firePoint.rotation);
            newFlame.GetComponent<Flame>().Parent = gameObject;
        }

        void Update()
        {
            soundTimer -= Time.deltaTime;
            if (Owner == null && Firing)
            {
                Firing = false;
            }
            if (Firing && Time.time > 1 / ShotsPerSecond + timer)
            {
                
                if (soundTimer <= 0)
                {
                    shotSound.Play();
                    soundTimer = 5f;
                }
                Fire();
                timer = Time.time;
            }
            else if (Firing == false)
            {
                soundTimer = 0;
                shotSound.Stop();
            }
        }
    }
}