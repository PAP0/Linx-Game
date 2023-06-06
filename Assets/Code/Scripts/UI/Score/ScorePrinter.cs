using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script sets the MaxScore & Prints out the CurrentScore to the specified UI Slider.
/// </summary>
public class ScorePrinter : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("Reference to the ScriptableObject that holds the CurrentScore.")]
    [SerializeField] private ScoreScriptableObject ScoreHolder;

    [Tooltip("Reference to the slider that will show the CurrentScore in the Game.")]
    [SerializeField] private Slider ScoreSlider;

    #endregion

    #region UnityEvents

    // When the script is initialized.
    private void Start()
    {
        // Resets the score at the start of the game.
        ScoreHolder.ScoreValue = 0;

        // Sets the MaxValue to the amount of objects spawned in.
        ScoreSlider.maxValue = ScoreHolder.TotalObjects;
    }

    // Checks every frame.
    private void Update()
    {
        // Prints the updated CurrentScore taken from the ScriptableObject.
        ScoreSlider.value = ScoreHolder.ScoreValue;
    }

    #endregion
}
