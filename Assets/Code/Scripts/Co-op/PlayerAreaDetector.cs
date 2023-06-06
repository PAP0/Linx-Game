using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>  
/// This script checks if all the players are inside the escape area.
/// </summary>

public class PlayerAreaDetector : MonoBehaviour
{
    [Header("Player Input Reference")] [Tooltip("The Player Input Manager")] [SerializeField]
    private PlayerInputManager PlayerInputManager; // A reference to the PlayerInputManager.

    private List<GameObject> PlayersInArea = new List<GameObject>(); //List of the players that are currently in the area
    
    [SerializeField] private TMP_Text ScoreTxt;
    
    public ScoreScriptableObject ScoreScriptableObject;
    public GameObject YouWin;
    
    private void OnTriggerEnter(Collider other) // Checks and adds the amount of players inside collider and adds them to the PlayersInArea list.
    {
        if (other.CompareTag("Player")) // If the collider that enters is a player
        {
            // Check if the player is already in the list
            if (!PlayersInArea.Contains(other.gameObject))
            {
                // Add the player to the list of players in the area
                PlayersInArea.Add(other.gameObject);

                // Check if the required number of players are in the area
                if (PlayersInArea.Count >= PlayerInputManager.playerCount)
                {
                    Time.timeScale = 0f;
                    YouWin.SetActive(true);
                    ScoreTxt.text = ScoreScriptableObject.ScoreValue.ToString();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) // Checks if player has exited the collider and removes them from the PlayersInArea list.
    {
        if (other.CompareTag("Player")) // If the collider that exits is a player
        {
            // Remove the player from the list of players in the area
            PlayersInArea.Remove(other.gameObject);

            // Check if the required number of players are no longer in the area
            if (PlayersInArea.Count < PlayerInputManager.playerCount)
            {
                Debug.Log("Not enough players in the area!");
            }
        }
    }
    private IEnumerator LoadSceneWithTransition()
    {
        yield return new WaitForSeconds(1f);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}