using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PelletScript : MonoBehaviour
    {
        float distTraveled;
        Vector3 startPos;
        //Vector3 currentPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }

        void Start()
        {
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);

            if (distTraveled > 20)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerHit = other.GetComponent<PlayerController>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                playerHit.playerMovement.KnockBack(transform.position - rb.velocity, 10);
            }

            if (other.tag != "Weapon" && other.tag != "Pellet")
            {
                if (other.tag == "Pellet")
                {
                    Debug.Log("Collided with self lul");                 
                }
                Destroy(gameObject);
            }
            
        }
    }
}
