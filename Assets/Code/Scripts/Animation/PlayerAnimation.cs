using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator PlayerAnimator;
    private PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsWalking == true)
        {
            PlayerAnimator.SetBool("WalkingAnim", true);
            PlayerAnimator.SetBool("CircleWipe_Out", false);
        }

        if (playerController.IsWalking == false)
        {
            PlayerAnimator.SetBool("WalkingAnim", false);
            PlayerAnimator.SetBool("CircleWipe_Out", true);
        }
    }
}
