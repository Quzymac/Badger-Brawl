using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ExplosionDamage : MonoBehaviour
    {
        float damageTimer = 0.3f;

        public GameObject Weapon { get; set; }

       

        void Update()
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                GetComponent<Collider>().enabled = false;
            }
            if(damageTimer <= -1)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(Weapon.GetComponent<IWeapon>().Damage);
                playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position, 10);
            }
        }
    }
}
