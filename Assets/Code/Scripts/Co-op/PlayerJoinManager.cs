using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///----------------------------------------$$$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  
///----------------------------------------$$  __$$\ $$  __$$\ $$  __$$\ $$$ __$$\ 
///----------------------------------------$$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$\ $$ |
///----------Author------------------------$$$$$$$  |$$$$$$$$ |$$$$$$$  |$$\$$\$$ |
///----------Patryk Podworny---------------$$  ____/ $$  __$$ |$$  ____/ $$ \$$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      $$ |\$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      \$$$$$$  /
///----------------------------------------\__|      \__|  \__|\__|       \______/
/// 
/// <summary>  
/// This class changes the PlayerPrefab variant of the Player Input Manager every time a player joins so that every player has a different prefab/abilities.
/// </summary>
/// 
public class PlayerJoinManager : MonoBehaviour
{ 
    [Tooltip("The Player Input Manager")]
    [SerializeField] private PlayerInputManager PlayerInputManager; // A reference to the PlayerInputManager.
    [Tooltip("The Player Prefabs")]
    [SerializeField] private GameObject[] PlayerPrefabs; // An array of player prefabs to choose from.
    [Tooltip("The spawn points for players")]
    [SerializeField] private Transform[] SpawnPoints;
    private int CurrentPrefabIndex = 0; // Index of current prefab to use.
    private int NumPlayersJoined = 0; // Number of players that have joined.

    public void Start()
    {
        // Set the first player prefab when the scene is loaded in.
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];

        // Set the position of the player prefab to the first available spawn point.
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[0].position;

        // Skip the first prefab of the array so that it doesn't spawn in 2 players with the same prefab.
        CurrentPrefabIndex = 1; // Start with one player already joined.
        Time.timeScale = 0.000001f;
    }

    public void OnPlayerJoined()
    {  
        if (GetComponent<PlayerInputManager>().playerCount == 2)
        {
            Time.timeScale = 1f;
        }

        // Set the new player prefab.
        PlayerInputManager.playerPrefab = PlayerPrefabs[CurrentPrefabIndex];

        // Set the player's position to the next available spawn point.
        int spawnPointIndex = CurrentPrefabIndex % SpawnPoints.Length;
        PlayerInputManager.playerPrefab.transform.position = SpawnPoints[spawnPointIndex].position;

        // Set the prefab array index to the next prefab in the array.
        CurrentPrefabIndex = (CurrentPrefabIndex + 1) % PlayerPrefabs.Length;
    }
}
