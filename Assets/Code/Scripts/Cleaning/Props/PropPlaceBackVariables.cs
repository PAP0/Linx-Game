using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPlaceBackVariables : MonoBehaviour
{
    [Tooltip("The predefined gameobject we want to detect")]
    [SerializeField] public GameObject TargetObject; // The predefined gameobject we want to detect
    [Tooltip("The bool that changes depending if the object is in the area or nott")]
    [SerializeField] public bool IsInPlace; // The bool that changes depending if the object is in the area or not
}
