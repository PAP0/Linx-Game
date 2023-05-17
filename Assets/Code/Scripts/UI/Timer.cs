using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] private float TimeLeft;

    [SerializeField] private TMP_Text TimerTxt;
    [SerializeField] private GameObject[] ObjectsToEnable;
    [SerializeField] private float[] EnableTimes;
    [SerializeField] private Animator ElevatorAnimator;
    
    public bool TimerOn = false;

    void Start()
    {
        ElevatorAnimator.speed = 0;
        TimerOn = false;
    }

    void Update()
    {
        if (TimerOn)
        {
            ElevatorAnimator.speed = 1;
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
