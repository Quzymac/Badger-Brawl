using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class BoomerangBullet : MonoBehaviour
    {
        public float ProjectileSpeed { get; } = 10f;
        public GameObject Owner { get; set; }
        public GameObject Parent { get; set; }
        public float Damage { get; set; }

        MeshRenderer boomerang;
        GameObject owner;

        Rigidbody rb;
        public Transform itemToRotate;
        Vector3 locationInFrontOfPlayer;
        Vector3 lastWeaponPos;
        Vector3 startPos;
        float distTraveled;
        bool go;

        [SerializeField]
        Collider boomerangerColl;

        void Start()
        {
            StartCoroutine("ColliderOff");
            go = true;
            itemToRotate = gameObject.transform.GetChild(0);
            owner = Owner.GetComponent<IWeapon>().Owner;
            boomerang = Parent.GetComponent<Boomerang>().boomerangerMesh;

            boomerang.enabled = false;
            locationInFrontOfPlayer = Parent.transform.position + Parent.transform.forward * 10f;
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);
            itemToRotate.transform.Rotate(0, 0, Time.deltaTime * 600);

            rb.velocity -= (transform.position - locationInFrontOfPlayer);
            rb.velocity = rb.velocity.normalized * ProjectileSpeed;

            if (distTraveled > 5)
            {
                go = false;
                locationInFrontOfPlayer = Parent.transform.position;
                if (transform.position == Parent.transform.position)
                {
                    Destroy(this.gameObject);
                    boomerang.enabled = true;
                    
                }

            }
            if (!go && Vector3.Distance(Parent.transform.position, transform.position) < 1f)
            {
                Destroy(this.gameObject);
                boomerang.enabled = true;
                
            }
        }
        IEnumerator ColliderOff()
        {
            boomerangerColl.enabled = false;
            yield return new WaitForSeconds(0.2f);
            boomerangerColl.enabled = true;
        }

        Vector3 offset = new Vector3(0.5f, 0, 0);
        Vector3 negativeOffset = new Vector3(-0.5f, 0, 0);
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Platform")
            {
                if (transform.position.x >= 0)
                {
                    Parent.transform.position = transform.position + negativeOffset;
                }
                else
                {
                    Parent.transform.position = transform.position + offset;
                }

                owner.GetComponent<ControllerInputs>().DropWeapon();
                boomerang.enabled = true;
                Destroy(this.gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                if (playerHit.Team != Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team)
                {
                    playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                    playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position - rb.velocity, Parent.GetComponent<IWeapon>().KnockBackPower);

                }

                if (transform.position.x >= 0)
                {
                    Parent.transform.position = transform.position + negativeOffset;
                }
                else
                {
                    Parent.transform.position = transform.position + offset;
                }
                owner.GetComponent<ControllerInputs>().DropWeapon();
                boomerang.enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
}