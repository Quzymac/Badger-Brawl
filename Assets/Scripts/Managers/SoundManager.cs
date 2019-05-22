using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] List<AudioClip> deathSounds = new List<AudioClip>();
        [SerializeField] GameObject deathSound;
        AudioSource audioSource;

        public void PlayDeathSounds(PlayerScript deadPlayer)
        {

            if (deadPlayer.Team == PlayerScript.PlayerTeam.badger)
            {               
                audioSource.clip = deathSounds[Random.Range(0, 1)];
                audioSource.Play();
            }
            else if (deadPlayer.Team == PlayerScript.PlayerTeam.human) //olika för female och male human så småning om
            {
                audioSource.clip = deathSounds[2];               
                audioSource.Play();
            }

        }

        void Start()
        {
            audioSource = deathSound.GetComponent<AudioSource>();
        }

        void Update()
        {

        }
    }
}
