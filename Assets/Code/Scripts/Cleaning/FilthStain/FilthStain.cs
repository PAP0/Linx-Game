using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class FilthStain : MonoBehaviour
{
    public Vacuum vacuumScript;

    [Header("Garbage")]
    public bool IsExposed;
    public bool HiddenFilth;

    [SerializeField] private float Range;
    [SerializeField] private ScoreScriptableObject ScoreHolder;

    void Update()
    {
        GameObject vacuumGuy = GameObject.FindGameObjectWithTag("AbilityBarrierVac");
        float distanceVacuum = Vector3.Distance(transform.position, vacuumGuy.transform.position);
        if (distanceVacuum <= Range && vacuumScript.IsSucking && !HiddenFilth)
        {
            Destroy(gameObject);
            ScoreHolder.ScoreValue++;
        }

        if (distanceVacuum <= Range && vacuumScript.IsSucking && IsExposed && IsExposed)
        {
            Destroy(gameObject);
            ScoreHolder.ScoreValue++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
