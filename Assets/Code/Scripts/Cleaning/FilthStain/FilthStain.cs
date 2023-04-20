using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilthStain : MonoBehaviour
{
    [Header("Blood")]
    [SerializeField] private bool IsBloodStain;

    [Header("Garbage")]
    [SerializeField] private bool IsGarbagePatch;

    private Animator BloodAnimator;
    private int CleanScore;

    private void Start()
    {
        BloodAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Char_VacuumGuy(Clone)" && IsGarbagePatch)
        {
            StartCoroutine(Fade());
        }

        if (other.name == "Mop_Char_Pref(Clone)" && IsBloodStain)
        {
            StartCoroutine(Fade());
        }
    }
    IEnumerator Fade()
    {
        BloodAnimator.SetTrigger("IsSoaped");
        CleanScore++;
        Debug.Log(CleanScore);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
