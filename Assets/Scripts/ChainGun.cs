using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ChainGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 3f;
        public float ShotsPerSecond { get; } = 6;
        public float BulletSpeed { get; } = 10f;     
        bool canFire = true;

        [SerializeField] float buildUpTime = 1f;

        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        public float ProjectileSpeed
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        IEnumerator FireTimer(float timer)
        {
            canFire = false;
            yield return new WaitForSeconds(timer);
            canFire = true;
        }

        public void Fire()
        {
            if (canFire)
            {
                GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.GetComponent<TestBullet>().Parent = gameObject;
                StartCoroutine(FireTimer(1 / ShotsPerSecond));
            }
        }

        void Start()
        {

        }

        public float nextAction;
        public float Timer = 1f;

        void Update()
        {
            nextAction += Time.deltaTime;
            if (nextAction >= Timer)
            {
                Fire();
                nextAction = 0;
                
            }
        }
    }
}
