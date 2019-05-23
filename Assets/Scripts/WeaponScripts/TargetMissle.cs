using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TargetMissle : MonoBehaviour
    {
        Vector3 startPos;
        Rigidbody rb;
        public GameObject Parent { get; set; }
        [SerializeField] GameObject Explosion; 
        [SerializeField]GameManager gameManager;
        public GameObject enemyPosition;
        public float Damage { get; set; }
       

        List<PlayerScript> enemies = new List<PlayerScript>();
        PlayerScript.PlayerTeam team;

        float time;
        float minDistance = 1000;
        float seekingDelay = 1f;
        float strenghtOfSeeking;
        float radius = 1f;


        void Start()
        {
            time = 0.0f;
            strenghtOfSeeking = 0.0f;
            team = Parent.GetComponent<IWeapon>().Owner.GetComponent<PlayerScript>().Team;
            gameManager = FindObjectOfType<GameManager>();
            
            enemyPosition = FindingEnemy();
            startPos = transform.position;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * Parent.GetComponent<IWeapon>().ProjectileSpeed;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), Parent.GetComponent<IWeapon>().Owner.GetComponent<Collider>(), true);
        }

        private void Update()
        {
            time += Time.deltaTime;

            if (enemyPosition != null)
            {
                if (time < seekingDelay)
                {
                    strenghtOfSeeking = time; //seekingStrenght from 0 to full strenght
                }
                rb.velocity -= (transform.position - enemyPosition.transform.position) * Parent.GetComponent<TargetGun>().SeakingStrenght * strenghtOfSeeking;
                rb.velocity = rb.velocity.normalized * Parent.GetComponent<IWeapon>().ProjectileSpeed;
                Vector3 vel = enemyPosition.transform.position - rb.velocity;
                transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            }
            if (time > Parent.GetComponent<TargetGun>().MaxTravelTime)
            {
                GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                explosion.GetComponent<ExplosionDamage>().ParticleScale(radius);
                explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<IWeapon>().Damage;
                explosion.GetComponent<ExplosionDamage>().KnockBackPower = Parent.GetComponent<IWeapon>().KnockBackPower;

                explosion.GetComponent<ExplosionDamage>().Weapon = Parent;
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                if (playerHit.Team != team)
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.GetComponent<ExplosionDamage>().ParticleScale(radius);
                    explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<IWeapon>().Damage;
                    explosion.GetComponent<ExplosionDamage>().KnockBackPower = Parent.GetComponent<IWeapon>().KnockBackPower;

                    explosion.GetComponent<ExplosionDamage>().Weapon = Parent;
                    Destroy(gameObject);
                }
            }
            else
            {
                if (other.tag != "Weapon" && other.tag != "Flame")
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.GetComponent<ExplosionDamage>().ParticleScale(radius);
                    explosion.GetComponent<ExplosionDamage>().Damage = Parent.GetComponent<IWeapon>().Damage;
                    explosion.GetComponent<ExplosionDamage>().KnockBackPower = Parent.GetComponent<IWeapon>().KnockBackPower;

                    explosion.GetComponent<ExplosionDamage>().Weapon = Parent;
                    Destroy(gameObject);
                }
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
