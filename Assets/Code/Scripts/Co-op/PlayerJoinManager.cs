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
    public PlayerInputManager playerInputManager; // A reference to the PlayerInputManager.
    [Tooltip("The Player Prefabs")]
    public GameObject[] playerPrefabs; // An array of player prefabs to choose from.
    
    private int currentPrefabIndex = 0; // Index of current prefab to use.

    public void Start()
    {
        // Set the first player prefab when the scene is loaded in.
        playerInputManager.playerPrefab = playerPrefabs[0];
        // Skip the first prefab of the array so that it doesn't spawn in 2 players with the same prefab.
        currentPrefabIndex = 1;
    }

    public void OnPlayerJoined()
    {
        // Set the new player prefab.
        playerInputManager.playerPrefab = playerPrefabs[currentPrefabIndex];
        // Set the prefab array index to the next prefab in the array.
        currentPrefabIndex = (currentPrefabIndex + 1) % playerPrefabs.Length;
    }
}
