using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    [SerializeField] private float Waittime = 5f;

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {

        yield return new WaitForSeconds(Waittime);

        SceneManager.LoadScene(1);  

    }

}