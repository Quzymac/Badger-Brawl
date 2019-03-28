using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnPosition : MonoBehaviour //trigger för vapnet som kollar om det finns ett vapen där.
{
    public bool occupied = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && occupied == false)
        {
            occupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        occupied = false;
        Debug.Log("Bajs");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Weapon")
        {
            Physics.IgnoreCollision(other, gameObject.GetComponent<Collider>());
        }
    }
}
