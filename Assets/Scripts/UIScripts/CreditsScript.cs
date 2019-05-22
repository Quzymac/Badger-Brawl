using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] GameObject panelOne;
    [SerializeField] GameObject panelTwo;
    [SerializeField] GameObject panelThree;

    private void OnEnable()
    {
        panelOne.SetActive(true);
        panelTwo.SetActive(false);
        panelThree.SetActive(false);
    }
    public void NextOne()
    {
        panelTwo.SetActive(true);
        panelOne.SetActive(false);
    }

    public void NextTwo()
    {
        panelThree.SetActive(true);
        panelTwo.SetActive(false);
    }


    public void PreviousOne()
    {
        panelOne.SetActive(true);
        panelTwo.SetActive(false);      
    }

    public void PreviousTwo()
    {
        panelTwo.SetActive(true);
        panelThree.SetActive(false);        
    }
 

}
