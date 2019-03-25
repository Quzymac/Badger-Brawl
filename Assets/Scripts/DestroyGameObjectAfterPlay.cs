using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectAfterPlay : MonoBehaviour
{
    private float totalTimeBeforeDestroy;
    [SerializeField] AudioClip pistolShot;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        totalTimeBeforeDestroy = source.clip.length;
    }


    void Update()
    {
        totalTimeBeforeDestroy -= Time.deltaTime;

        if(totalTimeBeforeDestroy <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
