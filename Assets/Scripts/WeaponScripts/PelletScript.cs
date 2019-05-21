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
        PlayerScript.PlayerTeam team;

        void Start()
        {
            team = Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team;
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
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                if (playerHit.Team != team)
                {
                    Parent.GetComponent<ShotgunScript>().amountHit[playerHit.playerNumber - 1]++;
                    Parent.GetComponent<ShotgunScript>().MultiplePlayerHit(playerHit);
                    playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                    playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position - rb.velocity, Parent.GetComponent<IWeapon>().KnockBackPower);
                }
            }
            else
            {
                if (other.tag != "Weapon" && other.tag != "Pellet")
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
