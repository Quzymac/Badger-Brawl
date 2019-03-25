using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Shuriken : MonoBehaviour, IWeapon
    {
        private int framesBeforeNextShot = 5;
        private int currentShotFrame = 0;
        public float Damage { get; } = 5f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;


        float timer = 0f;

        [SerializeField] private GameObject shotSound;
        [SerializeField] Transform firePoint;

        [SerializeField] GameObject shurikenBull;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
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
            if(currentShotFrame == 0)
            {

            }
            GameObject gunShot = Instantiate(shotSound, this.transform.position, this.transform.rotation) as GameObject;
            GameObject newBullet = Instantiate(shurikenBull, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<ShurikenBullet>().Parent = gameObject;
        }
    }
}
