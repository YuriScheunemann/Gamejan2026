using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Parallax")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private RiverScroller riverScroller;

    private void Start()
    {
        riverScroller = FindFirstObjectByType<RiverScroller>();

        if (riverScroller == null)
        {
            Debug.LogWarning(
                "ObstacleCollision: RiverScroller não encontrado na cena."
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        Debug.Log("Player bateu no obstáculo!");

        // Desacelera o cenário
        if (riverScroller != null)
        {
            riverScroller.SlowDown(
                slowMultiplier,
                slowDuration
            );
        }

        // Desacelera todos os objetos que possuem ObstacleMovement
        ObstacleMovement[] obstacles =
            FindObjectsByType<ObstacleMovement>(
                FindObjectsSortMode.None
            );

        foreach (ObstacleMovement obstacle in obstacles)
        {
            obstacle.SlowDown(
                slowMultiplier,
                slowDuration
            );
        }

        // Destrói o obstáculo que bateu no player
        Destroy(gameObject);
    }
}