using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public interface IWeapon
    {
        float Damage { get; }
        float ShotsPerSecond { get; }
        float ProjectileSpeed { get; }

        bool Firing { get; set; }

        void Fire();
        GameObject Owner { get; set; }

        Transform RightHand { get; set; }
        Transform LeftHand { get; set; }
    }

    public class TestGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 2f;
        public float ProjectileSpeed { get; } = 20f;

        float currentShotsPerSecond;
        float shotTimer;

        float timer = 0f;
        public bool Firing { get; set; } = false;

        private AudioSource shotSound;
        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }



        public void Start()
        {
            shotSound = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            shotSound.Play();
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<TestBullet>().Parent = gameObject;
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
