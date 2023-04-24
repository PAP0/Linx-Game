using UnityEngine;
using TMPro;


public class ScorePrinter : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] private ScoreScriptableObject ScoreHolder;
    [Header("Scores")]
    [SerializeField] private TMP_Text ScoreText;
    //[SerializeField] private TMP_Text EndScoreText;

    private void Start()
    {
        ScoreHolder.ScoreValue = 0;
    }

    private void Update()
    {
        ScoreText.text = "Score: " + ScoreHolder.ScoreValue;
    }
}
