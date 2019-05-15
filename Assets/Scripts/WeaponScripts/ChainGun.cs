using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ChainGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 2f;
        public float ShotsPerSecond { get; } = 8f;
        public float ProjectileSpeed { get; } = 20f;
        public bool Firing { get; set; } = false;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 1.5f;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;

        [SerializeField] float buildUpTime = 0.2f;

        private AudioSource shotSound;

        float currentShotsPerSecond;
        [SerializeField] float minShotsPerSeconds = 2f;
        float timer = 0f;
        float prevTimer = 0f;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }

        [SerializeField] Transform firePoint;
        [SerializeField] GameObject bullet;
        [SerializeField] GameObject muzzleFlash;

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

            GameObject newParticleEffect = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            newParticleEffect.transform.parent = firePoint;
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

