using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RocketLauncher : MonoBehaviour, IWeapon
    {
        public float Damage { get; set; }
        [SerializeField] float ThisDamage;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 15f;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 15f;

        float currentShotsPerSecond;
        float shotTimer;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject rocket;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        public GameObject Owner { get; set; }

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;

        public void Start()
        {
            shotSound = GetComponent<AudioSource>();
            Damage = ThisDamage;
        }
        public void Fire()
        {
            shotSound.Play();
            GameObject newBullet = Instantiate(rocket, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<RocketBullet>().Parent = gameObject;
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
