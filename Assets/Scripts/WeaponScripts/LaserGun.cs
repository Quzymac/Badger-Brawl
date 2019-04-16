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

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }

        float timer = 0f;

        float holdCharge = 0f;
        float getChargeValue = 0f;
        float maxCharge = 2f;

        [SerializeField] private AudioSource shotSound;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject shurikenBull;


        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        void Start()
        {
            shotSound = GetComponent<AudioSource>();
        }


        void Update()
        {
            if (Firing == true)
            {
                holdCharge += Time.deltaTime;
                if (holdCharge >= maxCharge)
                {
                    RaycastHit hit;
                    Vector3 aim = firePoint.transform.TransformDirection(Vector3.forward) * 10;
                    Debug.DrawRay(firePoint.transform.position, aim, Color.green);
                    shotSound.Play();
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
            else if (Firing == false && holdCharge > 1)
            {
                getChargeValue = holdCharge;
                holdCharge = 0;
            }
            else if (Firing == false && holdCharge >= 5)
            {
                getChargeValue = holdCharge;

            }
        }
        public void Fire()
        {

        }
    }
}
