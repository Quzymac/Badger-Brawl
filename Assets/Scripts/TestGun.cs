using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{

    public class TestGun : MonoBehaviour, IWeapon

    {
        public float Damage { get; } = 5f;
        public float FireRate { get; } = 0.1f;
        public float BulletSpeed { get; } = 10f;

        bool canFire = true;

        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;

        public GameObject Owner { get; set; }

        public void Fire()
        {
            if (canFire)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                StartCoroutine(FireTimer(FireRate));
            }
        }
       
        IEnumerator FireTimer(float timer)
        {
            canFire = false;
            yield return new WaitForSeconds(timer);
            canFire = true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!canFire)
            {

            }
        }
        
    }
}
