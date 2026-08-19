using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;
    [SerializeField] private float spawnY = 10f;

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPipe();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnPipe()
    {
        if (pipePrefab == null) return;

        Vector2 pos = new Vector2(Random.Range(spawnXMin, spawnXMax), spawnY);
        Instantiate(pipePrefab, pos, Quaternion.identity);
    }
}   