using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] float minIntensity = 0;
    [SerializeField] float maxIntensity = 1;
    [SerializeField] float flickeringSpeed = 0.2f;
    [SerializeField] float minToMaxSpeed = 1f;
    [SerializeField] Light light;
    float targetIntensity;
    static float time = 0f;
    static float t = 0f;


    void Start()
    {
        targetIntensity = maxIntensity;
    }
    
    void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, targetIntensity, t);
        t += minToMaxSpeed * Time.deltaTime;
        time += Time.deltaTime;

        if (time > flickeringSpeed)
        {
            if(targetIntensity >= maxIntensity - 0.001f)
            {
                targetIntensity = minIntensity;
            }
            else
            {
                targetIntensity = maxIntensity;
            }
            time = 0.0f;
            t = 0.0f;
        }
    }
}
