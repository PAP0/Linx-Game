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


    private Animator BloodAnimator;

    private void Start()
    {
        BloodAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AbilityBarrierVac" && IsGarbagePatch)
        {
            ScoreHolder.ScoreValue++;
            Destroy(gameObject);
        }

        if (other.name == "AbilityBarrierMop" && IsBloodStain)
        {
            IsSoaped = true;
        }

        if (other.name == "AbilityBarrierVac" && IsBloodStain && IsSoaped)
        {
            StartCoroutine(Fade());
        }
    }
    IEnumerator Fade()
    {
        ScoreHolder.ScoreValue++;
        BloodAnimator.SetTrigger("IsSoaped");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
