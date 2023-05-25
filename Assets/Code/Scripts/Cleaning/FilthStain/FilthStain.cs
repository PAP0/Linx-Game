using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilthStain : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AbilityBarrierMop" && IsGarbagePatch)
        {
            IsBrushed = true;
        }

        if (other.name ==  "AbilityBarrierVac" && IsGarbagePatch && IsBrushed)
        {
            ScoreHolder.ScoreValue++;
            Destroy(gameObject);
        }

        if (other.name == "AbilityBarrierVac" && IsBloodStain)
        {
            IsSoaped = true;
        }

        if (other.name == "AbilityBarrierMop" && IsBloodStain && IsSoaped)
        {
            StartCoroutine(Fade());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AbilityBarrierMop" && IsBloodStain && IsSoaped)
        {
            StopCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        ScoreHolder.ScoreValue++;
        BloodAnimator.SetTrigger("IsSoaped");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
