using UnityEngine;

/// <summary>
/// This script spawns objects in random positions within specified constraints.
/// </summary>
public class FilthSpawner : MonoBehaviour
{
    #region Variables

    [Tooltip("Array that holds all the spawned FithStains in the game")]
    public FilthStain[] filthStain;

    [Header("References")]
    [Tooltip("Reference to the ScriptableObject that holds the CurrentScore.")]
    [SerializeField] private ScoreScriptableObject Score;

    [Header("Spawn Settings")]
    [Tooltip("The amount of chance there is for a object to spawn at a position.")]
    [SerializeField] private float SpawnChance;

    [Tooltip("The Different objects that can be spawned")]
    [SerializeField] private GameObject[] SpawnObjects;
    
    [Header("Raycast Settings")]
    [Tooltip("The Distance the object can be spawned from each other.")]
    [SerializeField] private float Distance;

    [Tooltip("The Maximum Range for the FilthSpawners raycast")]
    [SerializeField] private float Range;

    [Tooltip("The layers the objects can be spawn on")]
    [SerializeField] private LayerMask Mask;

    [Header("Spawn Contraints")]
    [Tooltip("The minimum X & Z positions the FilthSpawner can spawn between.")]
    [SerializeField] private Vector2 MinimumSpawnPosition;

    [Tooltip("The maximum X & Z positions the FilthSpawner can spawn between.")]
    [SerializeField] private Vector2 MaximumSpawnPosition;

    // Randomizes the SpawnObjects
    private int RandomIndex;

    #endregion

    #region UnityEvents

    // When the game is started.
    private void Start()
    {
        // Resets the MaxScore/TotalObjects in the Scene.
        Score.TotalObjects = 0;
        Spawn();
    }

    #endregion

    #region Public Events

    /// <summary>
    /// Finds the Vacuum player in the scene for every FiltStain..
    /// </summary>
    public void FindVacuumGuy()
    {
        filthStain = FindObjectsOfType<FilthStain>();
        foreach (FilthStain obj in filthStain)
        {
            obj.vacuumScript = GameObject.Find("Char_VacuumGuy(Clone)").GetComponent<Vacuum>();
        }
    }

    #endregion

    #region Private Events

    // Spawns objects in random positions within specified constraints.
    private void Spawn()
    {
        // Checks every X position between the Minimum, Maximum & Distance contraint.
        for (float x = MinimumSpawnPosition.x; x < MaximumSpawnPosition.x; x += Distance)
        {
            // Checks every Z position between the Minimum, Maximum & Distance contraint.
            for (float z = MinimumSpawnPosition.y; z < MaximumSpawnPosition.y; z += Distance)
            {
                if (Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out RaycastHit hit, Range, Mask))
                {
                    // Checks if the SpawnChance is bigger than the randomized number.
                    if (SpawnChance > Random.Range(0, 101))
                    {
                        Score.TotalObjects++;
                        // Spawns the object at the current randomized position.
                        Instantiate(SpawnObjects[RandomIndex], new Vector3(hit.point.x, hit.point.y + 0.03f, hit.point.z), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        // Picks a random object from the SpawnObjects.
                        RandomIndex = Random.Range(0, SpawnObjects.Length);
                    }
                }
            }
        }
    }

    #endregion
}
