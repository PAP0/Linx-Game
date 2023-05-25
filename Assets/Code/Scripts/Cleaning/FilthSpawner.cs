using System.Collections;
using UnityEngine;

public class FilthSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private ScoreScriptableObject Score;

    [Header("Spawn Settings")]
    [SerializeField] private float SpawnTime;
    [SerializeField] private float SpawnChance;
    [SerializeField] private GameObject[] SpawnObjects;
    
    [Header("Raycast Settings")]
    [SerializeField] private float Distance;
    [SerializeField] private float Range;
    [SerializeField] private LayerMask Mask;

    [Header("Spawn Contraints")]
    [SerializeField] private Vector2 MinimumSpawnPosition;
    [SerializeField] private Vector2 MaximumSpawnPosition;

    private int RandomIndex;
    // Start is called before the first frame update
    void Start()
    {
        Score.TotalObjects = 0;

        Spawn();

        StartCoroutine(SpawnOverTime());
    }

    private void Update()
    {
       
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
    IEnumerator SpawnOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);
            Instantiate(SpawnObjects[RandomIndex], new Vector3(Random.Range(MinimumSpawnPosition.x, MaximumSpawnPosition.x), -0.95f, Random.Range(MinimumSpawnPosition.y, MaximumSpawnPosition.y)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
            Score.TotalObjects++;
        }
    }
}
