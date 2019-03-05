using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IWeapon
    {
        float Damage { get; }
        float FireRate { get; }

        void Fire();
        GameObject Owner { get; set; }


    }
    public class PlayerController : MonoBehaviour
    {
        GameObject weapon;

        GameObject canPickUp;

        // Start is called before the first frame update
        void Start()
        {
            //weapon = FindObjectOfType<TestGun>().gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (weapon != null)
                {
                    weapon.GetComponent<IWeapon>().Fire();
                }
            }

            if (Input.GetKeyDown(KeyCode.M)) //testing
            {
                PickUpWeapon();
            }

            if (Input.GetKeyDown(KeyCode.N)) //testing
            {
                DropWeapon();
            }
        }
        void PickUpWeapon()
        {
            if(canPickUp != null)
            {
                if(weapon != null)
                {
                    DropWeapon();
                }
                weapon = canPickUp.gameObject;
                weapon.transform.position = transform.position;
                weapon.transform.rotation = transform.rotation;

                weapon.transform.parent = transform;
            }
        }
        void DropWeapon()
        {
            if (weapon != null)
            {
                weapon.transform.parent = null;
                weapon = null;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon")) //Testing only
            {
                canPickUp = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon")) //Testing only
            {
                canPickUp = null;
            }
        }
    }
}
