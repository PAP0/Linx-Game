using UnityEngine;
using UnityEngine.UI;

public class ScorePrinter : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] private ScoreScriptableObject ScoreHolder;
    [Header("Scores")]
    [SerializeField] private Slider ScoreSlider;
    //[SerializeField] private TMP_Text EndScoreText;

    private void Start()
    {
        ScoreHolder.ScoreValue = 0;

    }

    private void Update()
    {
        ScoreSlider.maxValue = ScoreHolder.TotalObjects;
        ScoreSlider.value = ScoreHolder.ScoreValue;
    }
}
