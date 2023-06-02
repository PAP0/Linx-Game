using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRange : MonoBehaviour
{
    public float maxDistance = 5f;

    [SerializeField] private Transform Player1;
    [SerializeField] private Transform Player2;
    [SerializeField] private PlayerController playerControllerMop;
    [SerializeField] private PlayerController playerControllerVac;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Check the distance between the two players
        float distance = Vector3.Distance(Player1.position, Player2.position);

        // If the distance exceeds the maximum distance, restrict the players' positions
        if (distance > maxDistance)
        {
            Vector3 direction = (Player2.position - Player1.position).normalized;
            Vector3 player1TargetPos = Player2.position - direction * (maxDistance-0.1f);
            Vector3 player2TargetPos = Player1.position + direction * (maxDistance-0.1f);

            Player1.position = player1TargetPos;
            Player2.position = player2TargetPos;
        }

        if (distance > (maxDistance+1))
        {
            playerControllerMop.Speed = 0.01f;
            playerControllerVac.Speed = 0.01f;
        }

        if (distance < maxDistance)
        {
            playerControllerVac.Speed = 5;
            playerControllerMop.Speed = 5;
        }

        // Update the cable line renderer
        lineRenderer.SetPosition(0, Player1.position);
        lineRenderer.SetPosition(1, Player2.position);

    }

    public void FindPlayers()
    {
        Player1 = GameObject.Find("Mop_Char_Pref(Clone)").transform;
        Player2 = GameObject.Find("Char_VacuumGuy(Clone)").transform;

        playerControllerMop = GameObject.Find("Mop_Char_Pref(Clone)").GetComponent<PlayerController>();
        playerControllerVac = GameObject.Find("Char_VacuumGuy(Clone)").GetComponent<PlayerController>();
    }
}
