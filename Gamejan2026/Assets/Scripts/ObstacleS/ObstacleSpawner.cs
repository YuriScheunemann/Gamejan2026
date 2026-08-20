using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;

        [Range(0, 100)]
        public int chance = 50;
    }

    [Header("Objetos Normais")]
    [SerializeField] private SpawnObject[] spawnObjects;

    [Header("Sapo")]
    [SerializeField] private GameObject frogPrefab;
    [SerializeField] private float frogSpawnInterval = 10f;

    [Header("Fita Cassete")]
    [SerializeField] private GameObject tapePrefab;
    [SerializeField] private float tapeSpawnDelay = 3f;

    [Header("Área de Spawn")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float spawnY = 6f;

    [Header("Tempo")]
    [SerializeField] private float spawnInterval = 2f;

    private TurtleCollector turtleCollector;

    private bool tapeSpawned = false;
    private bool tapeSpawnRoutineStarted = false;

    private void Start()
    {
        turtleCollector =
            FindFirstObjectByType<TurtleCollector>();

        StartCoroutine(
            NormalSpawnRoutine()
        );

        StartCoroutine(
            FrogSpawnRoutine()
        );

        StartCoroutine(
            TapeCheckRoutine()
        );
    }

    // =========================================================
    // OBJETOS NORMAIS
    // =========================================================

    private IEnumerator NormalSpawnRoutine()
    {
        while (true)
        {
            SpawnObjectIfPossible();

            yield return new WaitForSeconds(
                spawnInterval
            );
        }
    }

    private void SpawnObjectIfPossible()
    {
        if (spawnObjects == null ||
            spawnObjects.Length == 0)
        {
            return;
        }

        SpawnObject selectedObject =
            GetRandomObject();

        if (selectedObject == null ||
            selectedObject.prefab == null)
        {
            return;
        }

        float randomX =
            Random.Range(
                minX,
                maxX
            );

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                spawnY,
                0f
            );

        Instantiate(
            selectedObject.prefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private SpawnObject GetRandomObject()
    {
        int totalChance = 0;

        foreach (
            SpawnObject spawnObject
            in spawnObjects)
        {
            if (spawnObject.prefab != null &&
                spawnObject.chance > 0)
            {
                totalChance +=
                    spawnObject.chance;
            }
        }

        if (totalChance <= 0)
            return null;

        int randomValue =
            Random.Range(
                0,
                totalChance
            );

        int currentChance = 0;

        foreach (
            SpawnObject spawnObject
            in spawnObjects)
        {
            if (spawnObject.prefab == null ||
                spawnObject.chance <= 0)
            {
                continue;
            }

            currentChance +=
                spawnObject.chance;

            if (randomValue < currentChance)
            {
                return spawnObject;
            }
        }

        return null;
    }

    // =========================================================
    // SAPOS
    // =========================================================

    private IEnumerator FrogSpawnRoutine()
    {
        // Primeiro sapo aparece depois de 10 segundos.
        yield return new WaitForSeconds(
            frogSpawnInterval
        );

        while (true)
        {
            SpawnFrogIfNeeded();

            // Próximo sapo em 10 segundos.
            yield return new WaitForSeconds(
                frogSpawnInterval
            );
        }
    }

    private void SpawnFrogIfNeeded()
    {
        if (frogPrefab == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Frog Prefab não foi definido."
            );

            return;
        }

        if (turtleCollector == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: TurtleCollector não encontrado."
            );

            return;
        }

        // Depois de coletar os 5, não cria mais sapos.
        if (turtleCollector.CanSpawnTape)
            return;

        float randomX =
            Random.Range(
                minX,
                maxX
            );

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                spawnY,
                0f
            );

        GameObject frog =
            Instantiate(
                frogPrefab,
                spawnPosition,
                Quaternion.identity
            );

        // Garante que o sapo tenha o movimento
        // configurado pelo mesmo sistema dos obstáculos.
        ObstacleMovement movement =
            frog.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(
                GetMovementSpeed()
            );
        }

        Debug.Log(
            "Sapo spawnado!"
        );
    }

    // =========================================================
    // FITA
    // =========================================================

    private IEnumerator TapeCheckRoutine()
    {
        while (true)
        {
            if (!tapeSpawnRoutineStarted &&
                turtleCollector != null &&
                turtleCollector.CanSpawnTape)
            {
                tapeSpawnRoutineStarted = true;

                StartCoroutine(
                    SpawnTapeAfterDelay()
                );

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator SpawnTapeAfterDelay()
    {
        Debug.Log(
            "5 sapos coletados! " +
            "Fita será spawnada em 3 segundos."
        );

        yield return new WaitForSeconds(
            tapeSpawnDelay
        );

        SpawnTape();
    }

    private void SpawnTape()
    {
        if (tapeSpawned)
            return;

        if (tapePrefab == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Tape Prefab não foi definido."
            );

            return;
        }

        float randomX =
            Random.Range(
                minX,
                maxX
            );

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                spawnY,
                0f
            );

        GameObject tape =
            Instantiate(
                tapePrefab,
                spawnPosition,
                Quaternion.identity
            );

        ObstacleMovement movement =
            tape.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(
                GetMovementSpeed()
            );
        }

        tapeSpawned = true;

        Debug.Log(
            "Fita cassete spawnada!"
        );
    }

    // =========================================================
    // VELOCIDADE
    // =========================================================

    private float GetMovementSpeed()
    {
        ObstacleMovement[] obstacles =
            FindObjectsByType<ObstacleMovement>(
                FindObjectsSortMode.None
            );

        foreach (ObstacleMovement obstacle in obstacles)
        {
            if (obstacle != null)
            {
                return obstacle.GetSpeed();
            }
        }

        return 1f;
    }
}