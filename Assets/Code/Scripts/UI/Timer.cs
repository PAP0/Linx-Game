using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private bool timerOn = false;

    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private float[] enableTimes;

    void Start()
    {
        timerOn = true;
    }

    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);

                for (int i = 0; i < objectsToEnable.Length; i++)
                {
                    if (timeLeft <= enableTimes[i])
                    {
                        objectsToEnable[i].SetActive(true);
                    }
                }
            }
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
