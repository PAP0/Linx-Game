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
    private int CurrentPrefabIndex = 0; // Index of current prefab to use.

    public void Start()
    {
        // Set the first player prefab when the scene is loaded in.
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];
        // Skip the first prefab of the array so that it doesn't spawn in 2 players with the same prefab.
        CurrentPrefabIndex = 1;
    }

    public void OnPlayerJoined()
    {
        // Set the new player prefab.
        PlayerInputManager.playerPrefab = PlayerPrefabs[CurrentPrefabIndex];
        // Set the prefab array index to the next prefab in the array.
        CurrentPrefabIndex = (CurrentPrefabIndex + 1) % PlayerPrefabs.Length;
    }
}
