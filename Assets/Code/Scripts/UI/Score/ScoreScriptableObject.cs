using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class ScoreScriptableObject : ScriptableObject
{
    public int ScoreValue;
    public int TotalObjects;
}
