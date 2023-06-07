using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script makes the game fade in with a circle animation.
/// </summary>

public class CircleWipe : MonoBehaviour
{
    [Header("Current circle size.")]
    [Tooltip("This is the current size of the circle showing the game.")]
    [SerializeField] private float CircleSize = 0;

    private Animator CircleAnimator;
    private Image CircleImage;
    private readonly int CircleSizeId = Shader.PropertyToID("_Circle_Size");

    // When the script is started.
    void Start()
    {
        // Gets the animator from the circle.
        CircleAnimator = gameObject.GetComponent<Animator>();
        // Gets the image for the circle that needs to be expanded
        CircleImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the circle size in the material.
        CircleImage.materialForRendering.SetFloat(CircleSizeId, CircleSize);
    }
}
