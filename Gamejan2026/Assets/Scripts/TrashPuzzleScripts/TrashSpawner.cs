using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TrashPrefab
    {
        public GameObject prefab;
        public TrashType type;
    }

    public TrashPrefab[] trashPrefabs;
    public float spawnInterval = 2f;
    public float conveyorSpeed = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 1f, spawnInterval);
    }

    private void SpawnTrash()
    {
        if (trashPrefabs.Length == 0)
            return;

        TrashPrefab selectedTrash = trashPrefabs[
            Random.Range(0, trashPrefabs.Length)
        ];

        GameObject newTrash = Instantiate(
            selectedTrash.prefab,
            transform.position,
            Quaternion.identity
        );

        TrashItem trashItem = newTrash.GetComponent<TrashItem>();

        if (trashItem != null)
        {
            trashItem.type = selectedTrash.type;
            trashItem.conveyorSpeed = conveyorSpeed;
        }
    }
}
