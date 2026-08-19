using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Área de Spawn")]
    [SerializeField] private BoxCollider2D spawnArea;

    [Header("Obstáculos")]
    [SerializeField] private GameObject[] obstacleObjects;

    [Header("Spawn")]
    [SerializeField] private float spawnRate = 2f;

    [Header("Movimento")]
    [SerializeField] private float obstacleSpeed = 1f;

    [Header("Sapo")]
    [SerializeField] private GameObject sapoPrefab;
    [SerializeField] private float sapoSpawnRate = 10f;
    [SerializeField] private int saposNecessarios = 5;

    [Header("Fita Cassete")]
    [SerializeField] private GameObject fitaPrefab;
    [SerializeField, Range(0f, 100f)] private float chanceFita = 40f;

    private float spawnTime;
    private float sapoSpawnTime;

    private int saposSpawnados = 0;

    private TurtleCollector turtleCollector;

    private void Start()
    {
        spawnTime = Time.time + spawnRate;
        sapoSpawnTime = Time.time + sapoSpawnRate;

        turtleCollector =
            FindFirstObjectByType<TurtleCollector>();
    }

    private void Update()
    {
        // =========================
        // OBSTÁCULOS / FITA
        // =========================

        if (Time.time >= spawnTime)
        {
            spawnTime = Time.time + spawnRate;

            SpawnObstacle();

            // Verifica se pode tentar spawnar a fita
            TentarSpawnarFita();
        }

        // =========================
        // SAPOS
        // =========================

        if (saposSpawnados < saposNecessarios &&
            Time.time >= sapoSpawnTime)
        {
            sapoSpawnTime = Time.time + sapoSpawnRate;

            SpawnSapo();
        }
    }

    private void SpawnObstacle()
    {
        if (spawnArea == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Spawn Area não foi definida."
            );

            return;
        }

        if (obstacleObjects == null ||
            obstacleObjects.Length == 0)
        {
            Debug.LogWarning(
                "ObstacleSpawner: nenhum obstáculo foi definido."
            );

            return;
        }

        int index =
            Random.Range(0, obstacleObjects.Length);

        Bounds bounds = spawnArea.bounds;

        float randomX =
            Random.Range(
                bounds.min.x,
                bounds.max.x
            );

        float spawnY = bounds.max.y;

        Vector2 spawnPosition =
            new Vector2(
                randomX,
                spawnY
            );

        GameObject obstacle =
            Instantiate(
                obstacleObjects[index],
                spawnPosition,
                Quaternion.identity
            );

        ObstacleMovement movement =
            obstacle.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(obstacleSpeed);
        }
        else
        {
            Debug.LogWarning(
                "O obstáculo " +
                obstacle.name +
                " não possui ObstacleMovement."
            );
        }
    }

    private void SpawnSapo()
    {
        if (spawnArea == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Spawn Area não foi definida para o sapo."
            );

            return;
        }

        if (sapoPrefab == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Sapo Prefab não foi definido."
            );

            return;
        }

        Bounds bounds =
            spawnArea.bounds;

        float randomX =
            Random.Range(
                bounds.min.x,
                bounds.max.x
            );

        float spawnY =
            bounds.max.y;

        Vector2 spawnPosition =
            new Vector2(
                randomX,
                spawnY
            );

        GameObject sapo =
            Instantiate(
                sapoPrefab,
                spawnPosition,
                Quaternion.identity
            );

        ObstacleMovement movement =
            sapo.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(obstacleSpeed);
        }
        else
        {
            Debug.LogWarning(
                "O sapo " +
                sapo.name +
                " não possui ObstacleMovement."
            );
        }

        saposSpawnados++;
    }

    private void TentarSpawnarFita()
    {
        // Se não encontrou o TurtleCollector,
        // não tenta spawnar.
        if (turtleCollector == null)
            return;

        // Ainda não coletou os 5 sapos.
        if (!turtleCollector.CanSpawnTape)
            return;

        if (fitaPrefab == null)
        {
            Debug.LogWarning(
                "ObstacleSpawner: Fita Prefab não foi definido."
            );

            return;
        }

        // 40% de chance
        float sorteio =
            Random.Range(0f, 100f);

        if (sorteio > chanceFita)
            return;

        SpawnFita();
    }

    private void SpawnFita()
    {
        Bounds bounds =
            spawnArea.bounds;

        float randomX =
            Random.Range(
                bounds.min.x,
                bounds.max.x
            );

        float spawnY =
            bounds.max.y;

        Vector2 spawnPosition =
            new Vector2(
                randomX,
                spawnY
            );

        GameObject fita =
            Instantiate(
                fitaPrefab,
                spawnPosition,
                Quaternion.identity
            );

        ObstacleMovement movement =
            fita.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(obstacleSpeed);
        }
        else
        {
            Debug.LogWarning(
                "A fita " +
                fita.name +
                " não possui ObstacleMovement."
            );
        }

        Debug.Log("Fita cassete spawnada!");
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(
            spawnArea.bounds.center,
            spawnArea.bounds.size
        );
    }
}