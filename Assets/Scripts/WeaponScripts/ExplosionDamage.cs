using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ExplosionDamage : MonoBehaviour
    {
        float damageTimer = 0.3f;
        public float Damage { get; set; }
        public float KnockBackPower { get; set; }

        public GameObject Weapon { get; set; }

        [SerializeField] List<AudioClip> explosionSounds = new List<AudioClip>();
        [SerializeField] AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            GetRandomSound();
        }

        void Update()
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                GetComponent<Collider>().enabled = false;
            }
            if(damageTimer <= -1)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(Damage);
                playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position + playerHit.gameObject.transform.position, KnockBackPower);
            }
        }

        void GetRandomSound()
        {
            audioSource.clip = explosionSounds[Random.Range(0, explosionSounds.Count)];
            audioSource.Play();
        }
    }
}
