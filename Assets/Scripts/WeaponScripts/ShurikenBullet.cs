using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShurikenBullet : MonoBehaviour
    {
        float distTraveled;
        Vector3 lastPos;
        Vector3 currentPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }
        PlayerScript.PlayerTeam team;

        void Start()
        {
            lastPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            transform.Rotate(10, 0, 0);
            distTraveled += Vector3.Distance(transform.position, lastPos);
            lastPos = transform.position;

            if (distTraveled > 20)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                if (playerHit.Team != team)
                {
                    playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                    playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position - rb.velocity, Parent.GetComponent<IWeapon>().KnockBackPower);
                    Destroy(gameObject);
                }
            }
            else {
                if (other.tag != "Weapon")
                {
                    Debug.Log("destroy");
                    Destroy(gameObject);
                }
            }
        }
    }
}
