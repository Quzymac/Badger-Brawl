using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponDespawn : MonoBehaviour
{
    public WeaponSpawning weaponSpawnManager;
    [SerializeField] float timer;
    IWeapon weapon;
    [SerializeField] MeshRenderer mesh;
    float flashSpeed = 0.2f;
    float flashStart = 5f;
    bool flashingActive = false;

    Coroutine flashCorutine;

    float despawnTimer = 15f;
    void Start()
    {
        weapon = GetComponent<IWeapon>();
        timer = despawnTimer * Random.Range(1.0f, 2.0f);
    }

    void Update()
    {
        if (weapon.Owner == null)
        {
            timer -= Time.deltaTime;
            if (timer < flashStart && !flashingActive)
            {
                flashingActive = true;
                flashCorutine = StartCoroutine(Flashing());
            }
            if (timer < 0f)
            {
                Despawn();
            }
        }
        else // testing move to pickupwwweapon
        {
            if (flashCorutine != null)
            {
                StopCoroutine(flashCorutine);
                mesh.enabled = true;
            }
            flashingActive = false;
            timer = despawnTimer;
        }
    }
    IEnumerator Flashing()
    {
        float flashFasterOverTime = 0f;
        while (flashingActive)
        {
            yield return new WaitForSeconds(flashSpeed - flashFasterOverTime);
            mesh.enabled = false;
            flashFasterOverTime += 0.002f;
            yield return new WaitForSeconds((flashSpeed - flashFasterOverTime)* 0.75f);
            mesh.enabled = true;
        }
    }
    void Despawn() //remove weapon from list of weapons and destroys gameObject
    {
        weaponSpawnManager.DespawnWeapon(gameObject);

        //flashingActive = false;
        //despawnTimer = CONSTtimer;
        Debug.Log("DESPAWN " + gameObject.name);
    }
}
