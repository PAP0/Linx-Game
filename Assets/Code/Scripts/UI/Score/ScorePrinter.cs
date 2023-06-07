using UnityEngine;
using TMPro;

/// <summary>
/// This script sets the MaxScore & Prints out the CurrentScore to the specified UI Slider.
/// </summary>
public class ScorePrinter : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("Reference to the ScriptableObject that holds the CurrentScore.")]
    [SerializeField] private ScoreScriptableObject ScoreHolder;
    [Tooltip("Reference to the text that will show the CurrentScore in the Game.")]
    [SerializeField] private TMP_Text ScoreTxt;


    #endregion

    #region Unity Events

    // When the game is started.
    private void Start()
    {
        // Resets the score at the start of the game.
        ScoreHolder.ScoreValue = 0;
        // Display the current score.
        ScoreTxt.text = "Score: " + ScoreHolder.ScoreValue.ToString();
    }

    // Checks every frame.
    private void Update()
    {
        // Prints the updated CurrentScore taken from the ScriptableObject.
        ScoreTxt.text = "Score: " + ScoreHolder.ScoreValue.ToString();
    }

    #endregion
}