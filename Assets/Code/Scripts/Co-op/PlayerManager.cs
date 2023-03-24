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

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs = new GameObject[4]; // an array of player prefabs to spawn
    private List<GameObject> spawnedPlayers = new List<GameObject>(); // a list of spawned players

    private void Start()
    {
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // get the player index
        int playerIndex = playerInput.playerIndex;

        // check if the player prefab exists for this index
        if (playerIndex >= playerPrefabs.Length || playerPrefabs[playerIndex] == null)
        {
            Debug.LogError("No player prefab found for player index " + playerIndex);
            return;
        }

        // spawn the player prefab at the same position as the first player
        SpawnPlayer(playerIndex, transform.position);
    }

    private void SpawnPlayer(int playerIndex, Vector3 position)
    {
        // instantiate the player prefab at the specified position
        GameObject player = Instantiate(playerPrefabs[playerIndex], position, Quaternion.identity);

        // add the player to the list of spawned players
        spawnedPlayers.Add(player);
    }
}
