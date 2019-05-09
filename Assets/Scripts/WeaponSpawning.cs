using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponSpawning : MonoBehaviour
    {
        public List<GameObject> weapons = new List<GameObject>();
        public List<GameObject> spawnPositions = new List<GameObject>();
        List<GameObject> spawnedWeapons = new List<GameObject>();

        [SerializeField] Transform upperCollider;
        [SerializeField] Transform bottomCollider;

        [SerializeField] int maxNumberOfWeapons = 6;
        public int numberOfWeapons { get; set; } = 0;
        float nextWeaponSpawnTimer = 0.5f;

        [SerializeField] float timeBetweenSpawns = 2f;

        Transform lastWeapon;
        Vector3 startZize = new Vector3(0.15f, 0.15f, 0.15f);
        Vector3 fullSize = new Vector3(1f, 1f, 1f);
        [SerializeField] GameObject weaponSpawnEffect;
        float growthSpeed = 8f;

        void Update()
        {
            if (numberOfWeapons < maxNumberOfWeapons)
            {
                nextWeaponSpawnTimer -= Time.deltaTime;

                if (nextWeaponSpawnTimer < 0)
                {
                    nextWeaponSpawnTimer = timeBetweenSpawns;
                    StartCoroutine(SpawnWeapon());
                }
            }
            if(lastWeapon != null && lastWeapon.localScale.x < 1.0f)
            {
                lastWeapon.localScale = Vector3.Lerp(lastWeapon.localScale, fullSize, Time.deltaTime * growthSpeed);
            }
        }

        Vector3 CheckSpawnPosition() 
        {
            while (true)
            {
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                if (spawnPositions[randomSpawn].transform.position.y > bottomCollider.position.y + 3 && spawnPositions[randomSpawn].transform.position.y < upperCollider.position.y - 3 && spawnPositions[randomSpawn].GetComponent<WeaponSpawnPosition>().occupied == false)
                {
                    return spawnPositions[randomSpawn].transform.position;
                }
            }
        }

        IEnumerator SpawnWeapon()
        {
            GameObject effect = Instantiate(weaponSpawnEffect, CheckSpawnPosition(), Quaternion.identity);

            yield return new WaitForSeconds(0.5f);

            numberOfWeapons++;
            int randomWeapon = Random.Range(0, weapons.Count);
            GameObject specificWeapon = Instantiate(weapons[randomWeapon], effect.transform.position, Quaternion.Euler(0, 90, 0));
            specificWeapon.GetComponent<WeaponDespawn>().weaponSpawnManager = this;
            spawnedWeapons.Add(specificWeapon);

            lastWeapon = specificWeapon.transform;
            lastWeapon.localScale = startZize;
        }
        
        public void DespawnWeapon(GameObject weapon)
        {
            spawnedWeapons.Remove(weapon);
            numberOfWeapons--;
            Destroy(weapon);
        }

        public void ClearWeapons()
        {       
            numberOfWeapons = 0;
            foreach (GameObject weapon in spawnedWeapons)
            {
                if (weapon != null)
                {
                    Destroy(weapon);
                }
            }
            spawnedWeapons.Clear();
            foreach (GameObject spawnPos in spawnPositions)
            {
                spawnPos.GetComponent<WeaponSpawnPosition>().occupied = false;
            }
        }
    }
}
