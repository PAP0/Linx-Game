using UnityEngine;

/// <summary>
/// This ScriptableObjects holds variables for the Score and for easily adding points.
/// </summary>
[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class ScoreScriptableObject : ScriptableObject
{
    #region Variables

    [Header("Variables")]
    [Tooltip("Hold the the CurrentScore of the game")]
    public int ScoreValue;

    [Tooltip("Holds the total amount of objects / Max amount of points")]
    public int TotalObjects;

    #endregion
}
