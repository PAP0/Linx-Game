using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float TransitionDelayTime = 1f; // The time to delay before loading the next scene

    public void PlayGame()
    {
        StartCoroutine(LoadSceneWithTransition());
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneWithTransition()
    {
        yield return new WaitForSeconds(TransitionDelayTime);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
