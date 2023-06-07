using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script makes the intro scene for the game play, showing the publisher (in this case our team) logo before the game fully launches.
/// </summary>

public class SplashScreen : MonoBehaviour
{
    #region Variables

    [Header("Waiting time")]
    [Tooltip("this is the waiting time before the intro scene plays.")]
    [SerializeField] private float WaitTime = 5f;

    #endregion

    #region Unity Events

    // When the script is started.
    void Start()
    {
        // Starts the coroutine to go to the intro scene.
        StartCoroutine(WaitForIntro());
    }

    #endregion

    #region Coroutines

    // Waits for an amount of seconds before loading scene.
    IEnumerator WaitForIntro()
    {
        // Makes the game wait before the scene gets changed.
        yield return new WaitForSeconds(WaitTime);
        // Loads the intro scene.
        SceneManager.LoadScene(1);  
    }

    #endregion
}