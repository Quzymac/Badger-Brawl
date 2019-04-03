using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleEffects : MonoBehaviour
{
    public ParticleSystem[] ps;

    void Start()
    {
        ps = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (ParticleEffectsDonePlaying())
        {
            Destroy(gameObject);
        }
    }

    bool ParticleEffectsDonePlaying()
    {
        foreach (ParticleSystem particle in ps)
        {
            if (particle.IsAlive())
            {
                return false;
            }
        }
        return true;
    }
}