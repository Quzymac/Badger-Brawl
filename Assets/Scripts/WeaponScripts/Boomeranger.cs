using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Boomeranger : MonoBehaviour
    {
        public float ProjectileSpeed { get; } = 10f;
        public GameObject Owner { get; set; }
        public GameObject Parent { get; set; }
        public float Damage { get; set; }

        [SerializeField] GameObject boomerang;
        [SerializeField] GameManager gameManager;
        [SerializeField] GameObject owner;
        
        Rigidbody rb;
        [SerializeField] Transform itemToRotate;
        Vector3 locationInFrontOfPlayer;
        Vector3 lastWeaponPos;
        Vector3 startPos;
        float distTraveled;
        float seakingStrenght = 0.4f;
        bool go;      

        void Start()
        {
            go = true;
            itemToRotate = gameObject.transform.GetChild(0);
            gameManager = FindObjectOfType<GameManager>();
            owner = Owner.GetComponent<IWeapon>().Owner;
            boomerang = GameObject.Find("Boomeranger");

            boomerang.GetComponent<MeshRenderer>().enabled = false;
            locationInFrontOfPlayer = new Vector3(owner.transform.position.x, owner.transform.position.y + 1, 0) + owner.transform.forward * 10f;
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);
            itemToRotate.transform.Rotate(0, 0, Time.deltaTime * 600);
            //Vector3 vel = rb.velocity - locationInFrontOfPlayer;
            //Debug.Log(vel);
            rb.velocity -= (transform.position - locationInFrontOfPlayer);
            rb.velocity = rb.velocity.normalized * ProjectileSpeed;
            //transform.LookAt(locationInFrontOfPlayer - rb.velocity);

            if (distTraveled > 5)
            {
                go = false;
                locationInFrontOfPlayer = Parent.transform.position;
                if (transform.position == Parent.transform.position)
                {
                    boomerang.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(this.gameObject);
                }

            }
            if (!go && Vector3.Distance(Parent.transform.position, transform.position) < 1.5f)
            {
                boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
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

                owner.GetComponent<NewControllerInputs>().DropWeapon();
                boomerang.GetComponent<MeshRenderer>().enabled = true;
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
                    playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position - rb.velocity, 10);

                    Destroy(gameObject);
                }
            }

            if (other.tag == "Player")
            {
                if (transform.position.x >= 0)
                {
                    Parent.transform.position = transform.position + negativeOffset;
                }
                else
                {
                    Parent.transform.position = transform.position + offset;
                }

                owner.GetComponent<NewControllerInputs>().DropWeapon();
                boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
}