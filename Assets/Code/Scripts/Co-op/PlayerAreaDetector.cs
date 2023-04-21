using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaDetector : MonoBehaviour
{
    [Tooltip("Value that equals the amount of players needed in the area to finish the level")]
    [SerializeField] private int PlayersNeeded; //Value that equals the amount of players needed in the area to finish the level
    private List<GameObject> PlayersInArea = new List<GameObject>(); //List of the players that are currently in the area

    private void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Player"))
    {
        // Check if the player is already in the list
        if (!PlayersInArea.Contains(other.gameObject))
        {
            // Add the player to the list of players in the area
            PlayersInArea.Add(other.gameObject);

            // Check if the required number of players are in the area
            if (PlayersInArea.Count >= PlayersNeeded)
            {
                Debug.Log("All players are in the area!");
            }
        }
    }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the player from the list of players in the area
            PlayersInArea.Remove(other.gameObject);

            // Check if the required number of players are no longer in the area
            if (PlayersInArea.Count < PlayersNeeded)
            {
                Debug.Log("Not enough players in the area!");
            }
        }
    }
}
