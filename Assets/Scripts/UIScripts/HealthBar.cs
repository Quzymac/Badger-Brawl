using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        Transform bar;
        public PlayerScript SpecificPlayer { get; set; }
        float maxHealth;

        void Start()
        {
            Transform bar = transform.Find("Bar");
            if (SpecificPlayer == null)
            {
                gameObject.SetActive(false);
            }
        }

        void Update()
        {
            
        }

        void TakingDamage()
        {

        }

        public void SetSize(float sizeNormalized)
        {
            bar.localScale = new Vector3(sizeNormalized, 1f);
        }
    }
}
