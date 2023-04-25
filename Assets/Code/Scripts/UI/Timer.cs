using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float TimeLeft;
    [SerializeField] private bool TimerOn = false;

    [SerializeField] private TMP_Text TimerTxt;
    [SerializeField] private GameObject[] ObjectsToEnable;
    [SerializeField] private float[] EnableTimes;

    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);

                for (int i = 0; i < ObjectsToEnable.Length; i++)
                {
                    if (TimeLeft <= EnableTimes[i])
                    {
                        ObjectsToEnable[i].SetActive(true);
                    }
                }
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
