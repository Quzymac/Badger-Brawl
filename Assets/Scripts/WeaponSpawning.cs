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

        [SerializeField] Transform upperCollider;
        [SerializeField] Transform bottomCollider;

        void Start()
        {

        }

        void Update()
        {

        }
        Transform CheckSpawnPosition()
        {
            while (true)
            {
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                if (spawnPositions[randomSpawn].transform.position.y > bottomCollider.position.y && spawnPositions[randomSpawn].transform.position.y < upperCollider.position.y && !usedSpawnPositions.Contains(spawnPositions[randomSpawn].transform))
                {
                    usedSpawnPositions.Add(spawnPositions[randomSpawn].transform);
                    return spawnPositions[randomSpawn].transform;
                }
            }
        }
    }
}
