using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{

    public class TestGun : MonoBehaviour, IWeapon

    {
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 5f;
        public float ProjectileSpeed { get; } = 20f;

        bool canFire = true;

        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        public void Fire()
        {
            if (canFire)
            {
                GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.GetComponent<TestBullet>().Parent = gameObject;
                StartCoroutine(FireTimer(1/ShotsPerSecond));
            }
        }
       
        IEnumerator FireTimer(float timer)
        {
            canFire = false;
            yield return new WaitForSeconds(timer);
            canFire = true;
        }
        
        // Update is called once per frame
        void Update()
        {
            //move timer for fireRate here and remove corutine
        }
    }
}
