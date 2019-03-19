using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class camerafollowplayer : MonoBehaviour
    {
        Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerScript>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, player.position.y + 5, transform.position.z);
        }
    }
}