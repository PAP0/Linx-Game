using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This script is for handling the button inputs on the main menu.
/// </summary> 

public class MainMenu : MonoBehaviour
{
    [Header("Transition delay")]
    [Tooltip("This is the delay before the main menu loads into the next scene.")]
    [SerializeField] private float TransitionDelayTime = 1f;

    /// <summary>
    /// Starts the coroutine with a scene transition.
    /// </summary>
    public void PlayGame()
    {
        // This loads the next scene with the transition.
        StartCoroutine(LoadSceneWithTransition());
    }

    /// <summary>
    /// Closes/Quits the application.
    /// </summary>
    public void Quitgame()
    {
        // This closes the application.
        Application.Quit();
    }

    private IEnumerator LoadSceneWithTransition()
    {
        // Waits for a couple seconds before the new scene loads.
        yield return new WaitForSeconds(TransitionDelayTime);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
