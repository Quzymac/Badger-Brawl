using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponSpawning : MonoBehaviour
    {
        public List<GameObject> weapons = new List<GameObject>();
        public List<GameObject> spawnPositions = new List<GameObject>();
        List<Transform> usedSpawnPositions = new List<Transform>();
        List<GameObject> spawnedWeapons = new List<GameObject>();

        [SerializeField] Transform upperCollider;
        [SerializeField] Transform bottomCollider;
        public bool newRound { get; set; } = false;

        [SerializeField] int maxNumberOfWeapons = 6;
        public int numberOfWeapons { get; set; } = 0;
        float nextWeaponSpawnTimer = 0.5f;

        [SerializeField] float timeBetweenSpawns = 2f;


        void Update()
        {
            if (numberOfWeapons < maxNumberOfWeapons)
            {
                nextWeaponSpawnTimer -= Time.deltaTime;

                if (nextWeaponSpawnTimer < 0)
                {
                    SpawnWeapon();
                    nextWeaponSpawnTimer = timeBetweenSpawns;
                }
            }
        }

        Transform CheckSpawnPosition() 
        {
            while (true)
            {
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                if (spawnPositions[randomSpawn].transform.position.y > bottomCollider.position.y + 3 && spawnPositions[randomSpawn].transform.position.y < upperCollider.position.y - 3 && spawnPositions[randomSpawn].GetComponent<WeaponSpawnPosition>().occupied == false)
                {
                    return spawnPositions[randomSpawn].transform;
                }
            }
        }

        public void SpawnWeapon()
        {
            numberOfWeapons++;
            int randomWeapon = Random.Range(0, weapons.Count);
            GameObject specificWeapon = Instantiate(weapons[randomWeapon], CheckSpawnPosition().position, Quaternion.Euler(0, 90, 0));
            specificWeapon.GetComponent<WeaponDespawn>().weaponSpawnManager = this;
            spawnedWeapons.Add(specificWeapon);
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
            newRound = true;
        }
    }
}
