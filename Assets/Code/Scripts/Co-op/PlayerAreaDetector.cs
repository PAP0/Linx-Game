using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>  
/// This script checks if all the players are inside the escape area and handles the win screen.
/// </summary>

public class PlayerAreaDetector : MonoBehaviour
{
    #region Variables

    [Header("Game Score")]
    [Tooltip("The Score Scriptable Object")]
    public ScoreScriptableObject ScoreScriptableObject;
    [Tooltip("The score text that appears along the win screen")]
    [SerializeField] private TMP_Text ScoreTxt;

    [Header("Co-Op")] 
    [Tooltip("The Player Input Manager")]
    [SerializeField] private PlayerInputManager PlayerInputManager;

    [Header("Hud elements")]
    [Tooltip("A reference to the Win Screen that gets turned on when the players enter the area in time")]
    [SerializeField] private GameObject WinScreen;

    //List of the players that are currently in the area.
    private List<GameObject> PlayersInArea = new List<GameObject>();

    #endregion

    #region Unity Events

    // Check if something has entered the collider.
    private void OnTriggerEnter(Collider other)
    {
        // If the collider that enters is a player.
        if (other.CompareTag("Player")) 
        {
            // Check if the player is already in the list.
            if (!PlayersInArea.Contains(other.gameObject))
            {
                // Add the player to the list of players in the area.
                PlayersInArea.Add(other.gameObject);
                // If the required amount of players are in the area.
                if (PlayersInArea.Count >= PlayerInputManager.playerCount)
                {
                    // Stop the in-game time.
                    Time.timeScale = 0f;
                    // Turn on the Win screen.
                    WinScreen.SetActive(true);
                    // Display the final score.
                    ScoreTxt.text = ScoreScriptableObject.ScoreValue.ToString();
                }
            }
        }
    }

    // Check if something has exited the collider.
    private void OnTriggerExit(Collider other)
    {
        // If the collider that exits is a player.
        if (other.CompareTag("Player")) 
        {
            // Remove the player from the list of players in the area.
            PlayersInArea.Remove(other.gameObject);
        }
    }

    #endregion
}