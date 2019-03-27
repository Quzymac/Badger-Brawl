using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioScript : MonoBehaviour
{
    public AudioSource uiSounds;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnHover()
    {
        uiSounds.PlayOneShot(hoverSound);
    }

    public void OnClick()
    {
        uiSounds.PlayOneShot(clickSound);
    }
    
}
