using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>  
/// This script changes the PlayerPrefab variant of the Player Input Manager every time a player joins so that every player has a different prefab/abilities.
/// This also handles the "press to join" function so that the game only starts when enough players are joined in.
/// </summary>

public class PlayerJoinManager : MonoBehaviour
{
    #region Variables

    [Header("Player Input Manager Reference")]
    [Tooltip("The Player Input Manager")]
    // A reference to the PlayerInputManager.
    [SerializeField] private PlayerInputManager PlayerInputManager;

    [Header("Timer Reference")]
    [Tooltip("The Timer Script")]
    // A reference to the Timer script.
    [SerializeField] private Timer TimerScript;

    [Header("Co-Op Variables")]
    [Tooltip("The Player Prefabs")]
    // An array of player prefabs to choose from.
    [SerializeField] private GameObject[] PlayerPrefabs;
    [Tooltip("The spawn points for players")]
    [SerializeField] private Transform[] SpawnPoints;

    [Header("Join In Hud Elements")]
    [Tooltip("The Press To Join Hud Elements")]
    // An array of hud elements that are turned off depending on the player count.
    [SerializeField] private GameObject[] HudJoinElements;

    // Index of current prefab to use.
    private int CurrentPrefabIndex = 0;

    #endregion

    #region Unity Events

    // When the script is initialized.
    public void Start()
    {
        // Set the first player prefab when the scene is loaded in.
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];
        // Set the position of the player prefab to the first available spawn point.
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[0].position;
        // Skip the first prefab of the array so that it doesn't spawn in 2 players with the same prefab.
        CurrentPrefabIndex = 1;
    }

    #endregion

    #region Methods

    /// <summary>  
    /// This Method sets new playerPrefab, spawnPoint. Handles Player join in function.
    /// </summary>
    public void OnPlayerJoined()
    {
        // Set the new player prefab.
        PlayerInputManager.playerPrefab = PlayerPrefabs[CurrentPrefabIndex];
        // Set the player's position to the next available spawn point.
        int spawnPointIndex = CurrentPrefabIndex % SpawnPoints.Length;
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[spawnPointIndex].position;
        // Set the prefab array index to the next prefab in the array.
        CurrentPrefabIndex = (CurrentPrefabIndex + 1) % PlayerPrefabs.Length;

        // If the first player has joined.
        if (PlayerInputManager.playerCount > 0)
        {
            // Turn off the "press to join as player 1" prompt.
            HudJoinElements[0].SetActive(false);
            // If the second player joins.
            if (PlayerInputManager.playerCount == 2)
            {
                // Turn off the "press to join as player 2" prompt.
                HudJoinElements[1].SetActive(false);
                // Start the game and start the timer.
                TimerScript.TimerOn = true;
            }
        }
    }

    #endregion
}