using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{

    public class TestGun : MonoBehaviour, IWeapon

    {
        public float damage { get; } = 5f;
        public float fireRate { get; } = 1f;

        public float bulletSpeed { get; } = 10f;

        [SerializeField] Transform firePoint;

        [SerializeField] GameObject bullet;



        public void Fire()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            Debug.Log("tfyugih");
        }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
