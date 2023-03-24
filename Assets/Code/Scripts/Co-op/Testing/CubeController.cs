using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///----------------------------------------$$$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  
///----------------------------------------$$  __$$\ $$  __$$\ $$  __$$\ $$$ __$$\ 
///----------------------------------------$$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$\ $$ |
///----------Author------------------------$$$$$$$  |$$$$$$$$ |$$$$$$$  |$$\$$\$$ |
///----------Patryk Podworny---------------$$  ____/ $$  __$$ |$$  ____/ $$ \$$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      $$ |\$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      \$$$$$$  /
///----------------------------------------\__|      \__|  \__|\__|       \______/ 

public class CubeController : MonoBehaviour
{
    public float jumpForce = 5f; // the force to apply when jumping
    public Color spawnColor; // the color to apply when the cube is spawned

    public Rigidbody rb; // the rigidbody component of the cube
    public Renderer rend; // the renderer component of the cube

    void Start()
    {
        spawnColor = Random.ColorHSV();
        // get the rigidbody and renderer components of the cube
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        // set the color of the cube to the spawn color
        rend.material.color = spawnColor;
    }

    public void Jump()
    {
        // apply a vertical force to the rigidbody component to make the cube jump up
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
