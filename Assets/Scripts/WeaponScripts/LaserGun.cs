using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class LaserGun : MonoBehaviour, IWeapon
    {
        public float Damage { get; } = 20f;
        public float ShotsPerSecond { get; } = 1f;
        public float ProjectileSpeed { get; } = 20f;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 5f;

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
        [SerializeField] Transform particlePoint;

        [SerializeField] ParticleSystem beam;
        [SerializeField] ParticleSystem beamStart;

        LayerMask layerMask;

        public bool Firing { get; set; } = false;
        public GameObject Owner { get; set; }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            layerMask |= (1 << LayerMask.NameToLayer("Player"));
            layerMask |= (1 << LayerMask.NameToLayer("jumpingPlayer"));
        }
        void Update()
        {
            Debug.DrawRay(firePoint.position, firePoint.TransformDirection(Vector3.forward) *10,Color.red, 0.1f, true);
            if (Firing == true)
            {
                if (!beamStart.isPlaying)
                {
                    beamStart.Play();
                }
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
            if(Firing && Owner == null)
            {
                Firing = false;
            }
        }
        IEnumerator ShootLaser()
        {
            yield return new WaitForSeconds(0.1f);
            RaycastHit hit;
            Vector3 aim = firePoint.forward;

            if (Physics.Raycast(firePoint.transform.position, aim, out hit, 20f, layerMask))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    hit.collider.GetComponent<ControllerMovement>().KnockBack(transform.position, knockBackPower);
                    hit.collider.GetComponent<PlayerScript>().TakeDamage(Damage);
                }
            }
        }
        public void Fire()
        {
            beam.Play();
            
            audioSource.Play();
            holdCharge = 0;

            StartCoroutine(ShootLaser());
        }
    }
}
