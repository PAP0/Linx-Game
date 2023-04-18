using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipe : MonoBehaviour
{

    private Animator CircleAnimator;
    private Image CircleImage;
    private readonly int CircleSizeId = Shader.PropertyToID("_Circle_Size");

    [SerializeField] private float CircleSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        CircleAnimator = gameObject.GetComponent<Animator>();
        CircleImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CircleImage.materialForRendering.SetFloat(CircleSizeId, CircleSize);
    }
}
