using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Boomerang : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 25f;

        float timer = 0f;

        [SerializeField] private GameObject shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject boomer;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Fire();
            //}
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
            GameObject gunShot = Instantiate(shotSound, this.transform.position, this.transform.rotation) as GameObject;
            GameObject clone = Instantiate(boomer, firePoint.position, firePoint.rotation);
            //clone = Instantiate(boomer, new Vector3(transform.position.x, transform.position.y + 1, 0), transform.rotation) as GameObject;
            clone.GetComponent<BoomerangBullet>().Owner = gameObject;
        }
    }
}
