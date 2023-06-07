using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>  
/// This script handles the time limit in the game and the lose screen.
/// </summary>

public class Timer : MonoBehaviour
{
    #region Variables

    [Header("Timer settings")]
    [Tooltip("Starts/stops the timer")]
    public bool TimerOn = false;
    [Tooltip("Amount of time left")]
    [SerializeField] private float TimeLeft;
    [Tooltip("Hud element that represents the time left")]
    [SerializeField] private TMP_Text TimerTxt;
    
    [Header("Game Score")]
    [Tooltip("The Score Scriptable Object")]
    [SerializeField] private ScoreScriptableObject ScoreScriptableObject;
    [Tooltip("The score text that appears along the lose screen")]
    [SerializeField] private TMP_Text ScoreTxt;
    
    [Header("Object Enabling")]
    [Tooltip("Objects to enable")]
    [SerializeField] private GameObject[] ObjectsToEnable;
    [Tooltip("The times at which the objects will be enabled")]
    [SerializeField] private float[] EnableTimes;
    
    [Header("Elevator Animator")]
    [Tooltip("The animator that opens the elevat doors")]
    [SerializeField] private Animator ElevatorAnimator;
    
    [Header("Hud elements")]
    [Tooltip("A reference to the Lose Screen that gets turned on when the players enter the area in time")]
    [SerializeField] private GameObject GameOver;
    
    #endregion

    #region Unity Events

    // When the game starts.
    void Start()
    {
        // Stop/Do not play the animation.
        ElevatorAnimator.speed = 0;
        // Do not start the timer.
        TimerOn = false;
    }

    // Every frame.
    void Update()
    {
        // If the timer is on.
        if (TimerOn)
        {
            // Play the elevator animation.
            ElevatorAnimator.speed = 1;
            // If the timer is above 0 seconds
            if (TimeLeft > 0)
            {
                // Countdown the timer.
                TimeLeft -= Time.deltaTime;
                //Update the Ui text.
                UpdateTimer(TimeLeft);
                //Run a loop that iterates through the ObjectsToEnable loop.
                for (int i = 0; i < ObjectsToEnable.Length; i++)
                {
                    // Checks if the TimeLeft value is less than or equal to the EnableTimes specified.
                    if (TimeLeft <= EnableTimes[i])
                    {
                        // Enables the specified items in the array.
                        ObjectsToEnable[i].SetActive(true);
                    }
                }
            }
            else
            {
                // Set the timer to 0.
                TimeLeft = 0;
                // Stop the timer.
                TimerOn = false;
                // Stop the in-game time.
                Time.timeScale = 0f;
                // Turn on the Lose screen.
                GameOver.SetActive(true);
                // Display the final score.
                ScoreTxt.text = ScoreScriptableObject.ScoreValue.ToString();
            }
        }
    }
    

    #endregion

    #region Public Methods

    /// <summary>  
    /// This Method loads back to the Main Menu.
    /// </summary>
    public void BackToMenu()
    {
        // Load back to the main menu.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    #endregion

    #region Private Methods

    // Updates the Ui text to match the remaining time.
    private void UpdateTimer(float currentTime)
    {
        // Increase currentTime by 1 unit.
        currentTime += 1;
        // Sets every 60 seconds as 1 minute.
        float minutes = Mathf.FloorToInt(currentTime / 60);
        // Calculate remaining seconds after minute conversion.
        float seconds = Mathf.FloorToInt(currentTime % 60);
        // Sets the Ui text equal to the amount of minutes and seconds.
        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    #endregion
}