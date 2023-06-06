using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator DoorAnimator;
    [SerializeField] private string TargetPlayer;

    private void Start()
    {
        DoorAnimator.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DoorAnimator.speed = 1;
            Debug.Log("door trigger");
        }
    }
}
