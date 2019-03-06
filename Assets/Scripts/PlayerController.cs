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

        float jumpForce = 300f;

        Rigidbody rb;
        Vector3 jumpDir;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            jumpDir = new Vector3(0, jumpForce, 0);

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(jumpDir);
            }

            if (Input.GetKey(KeyCode.Space))
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
                weapon.GetComponent<Collider>().enabled = false;
            }
        }
        void DropWeapon()
        {
            if (weapon != null)
            {
                weapon.GetComponent<Collider>().enabled = true;
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
        public void TakeDamage(float damage)
        {
            Debug.Log(damage + " damage taken");
        }
    }
}
