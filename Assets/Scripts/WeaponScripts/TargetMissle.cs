using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TargetMissle : MonoBehaviour
    {
        float distTraveled;
        Vector3 startPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }
        [SerializeField] GameObject Explosion; 
        [SerializeField]GameManager gameManager;
        public GameObject enemyPosition;
       

        List<PlayerScript> enemies = new List<PlayerScript>();

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            
            enemyPosition = FindingEnemy();
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            distTraveled = Vector3.Distance(transform.position, startPos);

            

            if (enemyPosition != null)
            {
                Vector3 vel = rb.velocity - enemyPosition.transform.position;
                Debug.Log(vel);
                rb.velocity -= (transform.position - enemyPosition.transform.position) * Parent.GetComponent<TargetGun>().SeakingStrenght;
                rb.velocity = rb.velocity.normalized * Parent.GetComponent<IWeapon>().ProjectileSpeed;
                transform.LookAt(enemyPosition.transform.position - rb.velocity);
            }
            if (distTraveled > 40)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Weapon")
            {
                GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                explosion.GetComponent<ExplosionDamage>().Weapon = Parent;

                Destroy(gameObject);
            }
        }

        GameObject FindingEnemy()
        {

            if (Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team == PlayerScript.PlayerTeam.badger)
            {
                enemies.AddRange(gameManager.Humans);

            }
            else if (Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team == PlayerScript.PlayerTeam.human)
            {
                enemies.AddRange(gameManager.Badgers);
            }
            Debug.Log(enemies.Count);
            GameObject closestEnemy = null;
            float minDistance = 1000;
            if (enemies.Count > 0)
            {
                foreach (PlayerScript enemy in enemies)
                {
                    if (enemy != null && Vector2.Distance(enemy.transform.position, transform.position) < minDistance)
                    {
                        closestEnemy = enemy.gameObject;
                        Debug.Log(closestEnemy);
                    }
                }
            }
            return closestEnemy;
        }
    }
}
