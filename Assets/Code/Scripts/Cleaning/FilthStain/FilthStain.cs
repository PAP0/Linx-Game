using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class FilthStain : MonoBehaviour
{
    [SerializeField] private float Range;       // The range within which the object is considered in range
    [SerializeField] private ScoreScriptableObject ScoreHolder;

    [Header("Blood")]
    [SerializeField] private bool IsBloodStain;
    [SerializeField] private bool IsSoaped;

    [Header("Garbage")]
    [SerializeField] private bool IsGarbagePatch;
    [SerializeField] private bool IsBrushed;
    
    private Animator BloodAnimator;

    private void Start()
    {
        BloodAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // Get the object with the specified tag
        GameObject vacuumGuy = GameObject.FindGameObjectWithTag("AbilityBarrierVac");
        GameObject mopGuy = GameObject.FindGameObjectWithTag("AbilityBarrierMop");
        // Calculate the distance between the reference object and the current object
        float distanceVacuum = Vector3.Distance(transform.position, vacuumGuy.transform.position);
        float distanceMop = Vector3.Distance(transform.position, mopGuy.transform.position);
        if (distanceMop <= Range && IsGarbagePatch)
        {
            IsBrushed = true;
        }
        if (distanceVacuum <= Range && IsGarbagePatch && IsBrushed)
        {
            Destroy(gameObject);
        }
        if (distanceVacuum <= Range && IsBloodStain)
        {
            IsSoaped = true;
        }
        if (distanceMop <= Range && IsBloodStain && IsSoaped)
        {   
            StartCoroutine(Fade());
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    IEnumerator Fade()
    {
        BloodAnimator.SetTrigger("IsSoaped");
        yield return new WaitForSeconds(3f);
        ScoreHolder.ScoreValue++;
        Destroy(gameObject);
    }
}
