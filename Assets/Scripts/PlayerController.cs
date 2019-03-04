using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IWeapon
    {
        float damage { get; }
        float fireRate { get; }

        void Fire();


    }
    public class PlayerController : MonoBehaviour
    {
        GameObject weapon;

        // Start is called before the first frame update
        void Start()
        {
            weapon = FindObjectOfType<TestGun>().gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.GetComponent<IWeapon>().Fire();

            }

        }
    }
}
