using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Shuriken : MonoBehaviour, IWeapon
    {
        private int framesBeforeNextShot = 5;
        private int currentShotFrame = 0;
        public float Damage { get; set; }
        [SerializeField] float thisDamage;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 2f;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.throwable;
        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }

        float timer = 0f;

        private AudioSource shotSound;
        [SerializeField] Transform firePoint;

        [SerializeField] GameObject shurikenBull;


        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            Damage = thisDamage;
            shotSound = GetComponent<AudioSource>();
            //shotSound.Stop();
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
            shotSound.Play();
            if(currentShotFrame == 0)
            {

            }
            GameObject newBullet = Instantiate(shurikenBull, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<ShurikenBullet>().Parent = gameObject;
        }
    }
}
