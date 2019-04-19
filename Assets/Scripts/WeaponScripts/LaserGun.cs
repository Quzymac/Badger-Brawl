using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class LaserGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 50f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;
        public GameObject Parent { get; set; }
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.Shooting;
        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }

        float timer = 0f;

        float holdCharge = 0f;
        float getChargeValue = 0f;
        float maxCharge = 2f;

        private AudioSource audioSource;
        [SerializeField] Transform firePoint;
        [SerializeField] ParticleSystem beam;
        [SerializeField] ParticleSystem beamStart;



        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
        }


        void Update()
        {
            if (Firing == true)
            {
                if(!beamStart.isPlaying)
                {
                    beamStart.Play();
;               }
                holdCharge += Time.deltaTime;
                if (holdCharge >= maxCharge)
                {
                    Fire();
                }
            }
            else if (Firing == false && holdCharge > 1)
            {
                getChargeValue = holdCharge;
                holdCharge = 0;
            }
            else if (Firing == false && holdCharge >= 5)
            {
                getChargeValue = holdCharge;

            }
            if (!Firing && beamStart.isPlaying)
            {
                beamStart.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
        public void Fire()
        {
            beam.Play();

            RaycastHit hit;
            Vector3 aim = firePoint.transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(firePoint.transform.position, aim, Color.green);
            audioSource.Play();
            holdCharge = 0;
            if (Physics.Raycast(firePoint.transform.position, aim, out hit, 10))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    PlayerScript playerHit = hit.collider.GetComponent<PlayerScript>();

                    Debug.Log("player hit");
                    playerHit.TakeDamage(Damage);
                }
            }
        }
    }
}
