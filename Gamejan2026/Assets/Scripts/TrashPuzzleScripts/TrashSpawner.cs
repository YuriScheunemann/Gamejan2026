using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TrashPrefab
    {
        public GameObject prefab;
        public TrashType type;
    }

    [Header("Lixos")]
    [SerializeField] private TrashPrefab[] trashPrefabs;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float conveyorSpeed = 2f;

    [Header("Fita Cassete")]
    [SerializeField] private GameObject tapePrefab;
    [SerializeField] private float tapeSpawnDelay = 3f;

    private bool stopSpawning;
    private bool tapeSpawned;

    private void Start()
    {
        InvokeRepeating(
            nameof(SpawnTrash),
            1f,
            spawnInterval
        );
    }

    private void SpawnTrash()
    {
        if (stopSpawning)
            return;

        if (trashPrefabs == null || trashPrefabs.Length == 0)
            return;

        TrashPrefab selectedTrash =
            trashPrefabs[
                Random.Range(0, trashPrefabs.Length)
            ];

        GameObject newTrash = Instantiate(
            selectedTrash.prefab,
            transform.position,
            Quaternion.identity
        );

        TrashItem trashItem =
            newTrash.GetComponent<TrashItem>();

        if (trashItem != null)
        {
            trashItem.type = selectedTrash.type;
            trashItem.conveyorSpeed = conveyorSpeed;
        }
    }

    public void TrashGoalReached()
    {
        if (stopSpawning)
            return;

        stopSpawning = true;

        Debug.Log(
            "TrashSpawner: objetivo de 15 lixos atingido!"
        );

        CancelInvoke(nameof(SpawnTrash));

        StartCoroutine(SpawnTapeAfterDelay());
    }

    private IEnumerator SpawnTapeAfterDelay()
    {
        yield return new WaitForSeconds(tapeSpawnDelay);

        SpawnTape();
    }

    private void SpawnTape()
    {
        if (tapeSpawned)
            return;

        if (tapePrefab == null)
        {
            Debug.LogWarning(
                "TrashSpawner: Fita Cassete não foi definida."
            );

            return;
        }

        tapeSpawned = true;

        GameObject tape = Instantiate(
            tapePrefab,
            transform.position,
            Quaternion.identity
        );

        TrashItem trashItem =
            tape.GetComponent<TrashItem>();

        if (trashItem != null)
        {
            trashItem.conveyorSpeed = conveyorSpeed;
        }

        Debug.Log(
            "TrashSpawner: Fita Cassete spawnada!"
        );
    }
}