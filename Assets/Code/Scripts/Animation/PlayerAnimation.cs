using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private PlayerController playerController;
    public float AnimationNumer = 0f;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("AnimateState", AnimationNumer);
        anim = GetComponentInChildren<Animator>();
    }

   public void Walking()
    {
        AnimationNumer = 0.5f;
    }

   public void Jumping()
    {
        AnimationNumer = 1f;
    }

   public void Idle()
    {
        AnimationNumer = 0f;
    }
}
