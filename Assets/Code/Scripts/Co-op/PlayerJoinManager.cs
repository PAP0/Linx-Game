using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>  
/// This script changes the PlayerPrefab variant of the Player Input Manager every time a player joins so that every player has a different prefab/abilities.
/// This also handles the "press to join" function so that the game only starts when enough players are joined in.
/// </summary>

public class PlayerJoinManager : MonoBehaviour
{ 
    [Header("Player Input Manager Reference")]
    [Tooltip("The Player Input Manager")]
    [SerializeField] private PlayerInputManager PlayerInputManager; // A reference to the PlayerInputManager.
    
    [Header("Timer Reference")]
    [Tooltip("The Timer Script")]
    [SerializeField] private Timer TimerScript; // A reference to the Timer script.
    
    [Header("Co-Op Variables")]
    [Tooltip("The Player Prefabs")]
    [SerializeField] private GameObject[] PlayerPrefabs; // An array of player prefabs to choose from.
    [Tooltip("The spawn points for players")]
    [SerializeField] private Transform[] SpawnPoints;
    
    [Header("Join In Hud Elements")]
    [Tooltip("The Press To Join Hud Elements")]
    [SerializeField] private GameObject[] HudJoinElements; // An array of hud elements that are turned off depending on the player count.
    private int CurrentPrefabIndex = 0; // Index of current prefab to use.
    private int NumPlayersJoined = 0; // Number of players that have joined.

    public void Start() // When the game is started
    {
        // Set the first player prefab when the scene is loaded in.
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];

        // Set the position of the player prefab to the first available spawn point.
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[0].position;

        // Skip the first prefab of the array so that it doesn't spawn in 2 players with the same prefab.
        CurrentPrefabIndex = 1; // Start with one player already joined.
    }

    public void OnPlayerJoined() // Sets new playerPrefab, spawnPoint. Handles Player join in function.
    {
        // Set the new player prefab.
        PlayerInputManager.playerPrefab = PlayerPrefabs[CurrentPrefabIndex];

        // Set the player's position to the next available spawn point.
        int spawnPointIndex = CurrentPrefabIndex % SpawnPoints.Length;
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[spawnPointIndex].position;

        // Set the prefab array index to the next prefab in the array.
        CurrentPrefabIndex = (CurrentPrefabIndex + 1) % PlayerPrefabs.Length;

        if (PlayerInputManager.playerCount > 0) // If the first player has joined.
        {
            HudJoinElements[0].SetActive(false); // Turn off the "press to join as player 1" prompt.

            if (PlayerInputManager.playerCount == 2) // If the second player joins.
            {
                HudJoinElements[1].SetActive(false); // Turn off the "press to join as player 2" prompt.
                TimerScript.TimerOn = true; // Start the game and start the timer.
            }
        }
    }
}
