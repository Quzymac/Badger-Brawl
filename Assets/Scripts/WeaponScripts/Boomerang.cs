using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Boomerang : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 15f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;
        public float KnockBackPower { get { return knockBackPower; } }
        [SerializeField] float knockBackPower = 2.5f;

        float timer = 0f;

        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;
        public MeshRenderer boomerangerMesh;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.throwable;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject boomer;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        void Start()
        {
            shotSound = GetComponent<AudioSource>();
            shotSound.Stop();
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
            shotSound.Play();
            GameObject clone = Instantiate(boomer, firePoint.position, firePoint.rotation);

            clone.GetComponent<Boomeranger>().Owner = gameObject;
            clone.GetComponent<Boomeranger>().Parent = gameObject;
        }
    }
}
