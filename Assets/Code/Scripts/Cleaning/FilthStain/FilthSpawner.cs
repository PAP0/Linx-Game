using System.Collections;
using UnityEngine;

public class FilthSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private ScoreScriptableObject Score;

    [Header("Spawn Settings")]
    [SerializeField] private float SpawnChance;
    [SerializeField] private GameObject[] SpawnObjects;

    [Header("Raycast Settings")]
    [SerializeField] private float Distance;
    [SerializeField] private float Range;
    [SerializeField] private LayerMask Mask;

    [Header("Spawn Constraints")]
    [SerializeField] private Vector2 MinimumSpawnPosition;
    [SerializeField] private Vector2 MaximumSpawnPosition;

<<<<<<< Updated upstream:Assets/Code/Scripts/Cleaning/FilthSpawner.cs
=======
    public FilthStain[] filthStain;

    public void FindVacuumGuy()
    {
        filthStain = FindObjectsOfType<FilthStain>();
        foreach (FilthStain obj in filthStain)
        {
            obj.vacuumScript = GameObject.Find("Char_VacuumGuy(Clone)").GetComponent<Vacuum>();
        }
    }

>>>>>>> Stashed changes:Assets/Code/Scripts/Cleaning/FilthStain/FilthSpawner.cs
    private int RandomIndex;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + new Vector3((MinimumSpawnPosition.x + MaximumSpawnPosition.x) / 2, 0, (MinimumSpawnPosition.y + MaximumSpawnPosition.y) / 2),
            new Vector3(MaximumSpawnPosition.x - MinimumSpawnPosition.x, 0.1f, MaximumSpawnPosition.y - MinimumSpawnPosition.y));
    }

    // Start is called before the first frame update
    void Start()
    {
        Score.TotalObjects = 0;

        Spawn();
    }

    private void Spawn()
    {
        for (float x = MinimumSpawnPosition.x; x < MaximumSpawnPosition.x; x += Distance)
        {
            for (float z = MinimumSpawnPosition.y; z < MaximumSpawnPosition.y; z += Distance)
            {
                if (Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out RaycastHit hit, Range, Mask))
                {
                    if (SpawnChance > Random.Range(0, 101))
                    {
                        Score.TotalObjects++;
                        Instantiate(SpawnObjects[RandomIndex], new Vector3(hit.point.x, hit.point.y + 0.03f, hit.point.z), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        RandomIndex = Random.Range(0, SpawnObjects.Length);
                    }
                }
            }
        }
    }
}
