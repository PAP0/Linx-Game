using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public float maxLength = 6.5f;

    private Transform VacuumGuy;
    private Transform BatteryGuy;
    private LineRenderer LineRenderer;

    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (VacuumGuy != null && BatteryGuy != null)
        {
            Vector3 player1Pos = VacuumGuy.position;
            Vector3 player2Pos = BatteryGuy.position;

            // Calculate the distance between players
            float distance = Vector3.Distance(player1Pos, player2Pos);

            // Check if the distance exceeds the maximum length
            if (distance > maxLength)
            {
                // Calculate the direction between players
                Vector3 direction = (player2Pos - player1Pos).normalized;

                // Set the position of the second player within the maximum length
                player2Pos = player1Pos + direction * maxLength;

                // Update the position of player2
                BatteryGuy.position = player2Pos;

                GameObject.Find("Mop_Char_Pref(Clone)").GetComponent<PlayerController>().CanWalk = false;
            }
            else
            {
                GameObject.Find("Mop_Char_Pref(Clone)").GetComponent<PlayerController>().CanWalk = true;
            }

            // Update the line renderer positions
            LineRenderer.SetPosition(0, player1Pos);
            LineRenderer.SetPosition(1, player2Pos);
        }
    }

    private void LateUpdate()
    {
        if (VacuumGuy == null)
        {
            VacuumGuy = GameObject.Find("Char_VacuumGuy(Clone)").transform;
        }
        if (BatteryGuy == null)
        {
            BatteryGuy = GameObject.Find("Mop_Char_Pref(Clone)").transform;
        }
    }
}
