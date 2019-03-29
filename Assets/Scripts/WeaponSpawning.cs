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
        int waitSpawnCounter = 0; 

        void Start()
        {
            
        }

        void Update()
        {
            waitSpawnCounter++;
            if (waitSpawnCounter >= 2)
            {
                if (numberOfWeapons < maxNumberOfWeapons)
                {
                    waitSpawnCounter = 0;
                    SpawnWeapon();
                }
            }
          
        }

        Transform CheckSpawnPosition() 
        {
            while (true)
            {
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                if (spawnPositions[randomSpawn].transform.position.y > bottomCollider.position.y && spawnPositions[randomSpawn].transform.position.y < upperCollider.position.y && spawnPositions[randomSpawn].GetComponent<WeaponSpawnPosition>().occupied == false)
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
            spawnedWeapons.Add(specificWeapon);
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
