using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
/// This script handles the cable/rope mechanic in the game. It makes sure that the players stay together
/// and cannot go too far apart.
/// </summary>

public class Cable : MonoBehaviour
{
    #region Variables

    [Header("Cable Length")]
    [Tooltip("The desired max length of the cable/rope")]
    public float maxLength = 6.5f;

    // Player1's transform, first end of the rope.
    private Transform VacuumGuy;
    // Player2's transform, second end of the rope.
    private Transform BatteryGuy;
    // LineRenderer which represents visual cable/rope.
    private LineRenderer LineRenderer;

    #endregion

    #region Unity Events

    // When the script is initialized.
    private void Start()
    {
        // Assign the LineRenderer variable to the scripts' object LineRenderer. 
        LineRenderer = GetComponent<LineRenderer>();
        // Assign the LineRenderers amount of positions that it will need to render between. 
        LineRenderer.positionCount = 2;
    }

    // Every frame.
    private void Update()
    {
        // If VacuumGuy and BatteryGuy are assigned.
        if (VacuumGuy != null && BatteryGuy != null)
        {
            // Set the players' Vector3 positions equal to the players' current positions.
            Vector3 player1Pos = VacuumGuy.position;
            Vector3 player2Pos = BatteryGuy.position;
            // Calculate the distance between the players.
            float distance = Vector3.Distance(player1Pos, player2Pos);
            // Check if the distance exceeds the max length.
            if (distance > maxLength)
            {
                // Calculate the direction between players.
                Vector3 direction = (player2Pos - player1Pos).normalized;
                // Set the position of the second player within the max length.
                player2Pos = player1Pos + direction * maxLength;
                // Update the position of player2.
                BatteryGuy.position = player2Pos;
                // Make the 2nd player unable to walk away while max distance is reached.
                GameObject.Find("Mop_Char_Pref(Clone)").GetComponent<PlayerController>().CanWalk = false;
            }
            else
            {
                // Make the 2nd player able to walk away while within max distance.
                GameObject.Find("Mop_Char_Pref(Clone)").GetComponent<PlayerController>().CanWalk = true;
            }

            // Update the line renderer positions.
            LineRenderer.SetPosition(0, player1Pos);
            LineRenderer.SetPosition(1, player2Pos);
        }
    }

    // After all Update methods are called.
    private void LateUpdate()
    {
        // If VacuumGuy is not assigned yet.
        if (VacuumGuy == null)
        {
            // Find object that has the specified name and assign it as a target.
            VacuumGuy = GameObject.Find("Char_VacuumGuy(Clone)").transform;
        }
        // If BatteryGuy is not assigned yet.
        if (BatteryGuy == null)
        {
            // Find object that has the specified name and assign it as a target.
            BatteryGuy = GameObject.Find("Mop_Char_Pref(Clone)").transform;
        }
    }

    #endregion
}