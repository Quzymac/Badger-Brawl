using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPause : MonoBehaviour
{
    public GameObject countdownCanvas;
    [SerializeField] Text countdownText;
    public float count;
    [SerializeField] float pauseStartTimer = 3f;
    [SerializeField] float gameStartTimer = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 0;
        countdownCanvas.SetActive(true);
        count = gameStartTimer + 0.49f;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownCanvas.activeInHierarchy)
        {
            countdownText.text = Mathf.Round(count + 0.49f).ToString();
            count -= Time.unscaledDeltaTime;
            if(count <= 0)
            {
                countdownCanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else if (!countdownCanvas.activeInHierarchy && count < 0)
        {
            count = pauseStartTimer;
        }
    }
}
