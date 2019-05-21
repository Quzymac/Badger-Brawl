using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GrenadeScript : MonoBehaviour, IWeapon
    {
        public float Damage { get; set; }
        [SerializeField] float ThisDamage;
        public float ShotsPerSecond { get; } = 0.5f;
        public float ProjectileSpeed { get; } = 25f;
        public bool Firing { get; set; } = false;
        public float KnockBackPower { get { return knockBackPower; } }

        [SerializeField] float knockBackPower = 20f;

        public TestGun.TypeOfWeapon typeOfWeapon { get; set; } = TestGun.TypeOfWeapon.throwable;
        public GameObject Owner { get; set; }
        float timer = 0f;
        public GameObject Parent { get; set; }

        float holdIncreaseThrow = 0.5f;
        float getHoldValue = 0f;
        float maxHoldThrow = 3f;
        float radius = 2f;
        [SerializeField] Transform leftHandPos;
        [SerializeField] Transform rightHandPos;

        public Transform RightHand { get { return rightHandPos; } set { rightHandPos = value; } }
        public Transform LeftHand { get { return leftHandPos; } set { leftHandPos = value; } }
        private AudioSource shotSound;
        Rigidbody rb;
        bool isThrown = false;
        bool startExplosion = false;
        [SerializeField] GameObject Explosion;
        [SerializeField] float blowUpTimer = 3f;

        void Start()
        {
            Damage = ThisDamage;
            rb = GetComponent<Rigidbody>();
            shotSound = GetComponent<AudioSource>();
            shotSound.Stop();
        }

        void Update()
        {
            if (Firing == true)
            {
                holdIncreaseThrow += Time.deltaTime;
                if (holdIncreaseThrow >= maxHoldThrow)
                {
                    startExplosion = true;
                    blowUpTimer = 0f;
                }
            }
            else if (Firing == false && holdIncreaseThrow > 0.5f)
            {
                getHoldValue = holdIncreaseThrow;
                holdIncreaseThrow = 0;
                isThrown = true;
            }
            else if (Firing == false && holdIncreaseThrow >= 5)
            {
                getHoldValue = holdIncreaseThrow;
                Instantiate(Explosion, transform.position, transform.rotation);
                GetComponent<WeaponDespawn>().Despawn(); //destroy and remove from list of weapons
            }

            if (isThrown == true)
            {
                isThrown = false;

                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;

                GetComponent<SphereCollider>().enabled = false;
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");

                Fire();
                Owner.GetComponent<ControllerInputs>().DropWeapon();

                startExplosion = true;
            
            }

            if (startExplosion == true)
            {
                blowUpTimer -= Time.deltaTime;
                if (blowUpTimer <= 0)
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.GetComponent<ExplosionDamage>().ParticleScale(radius);
                    explosion.GetComponent<ExplosionDamage>().Damage = Damage;
                    explosion.GetComponent<ExplosionDamage>().KnockBackPower = KnockBackPower;

                    GetComponent<WeaponDespawn>().Despawn(); //destroy and remove from list of weapons
                    startExplosion = false;
                }              
            }
        }

        public void Fire()
        {
            shotSound.Play();

            blowUpTimer = 5f;
            rb.AddForce(transform.forward * (ProjectileSpeed * (getHoldValue * 20)) + new Vector3(0, getHoldValue * 100, 0));
        }

        private void OnCollisionEnter(Collision collision)
        {
            blowUpTimer *= 0.8f;
        }
    }
}
