using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
                UpdateTimer(TimeLeft);

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
                TimeLeft = 0;
                TimerOn = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
