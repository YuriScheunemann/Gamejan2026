using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Desaceleração")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    [Header("Player")]
    [SerializeField] private string playerTag = "Player";

    private RiverScroller riverScroller;
    private ReactionHUD reactionHUD;

    private bool alreadyHit = false;

    private void Start()
    {
        riverScroller =
            FindFirstObjectByType<RiverScroller>();

        reactionHUD =
            FindFirstObjectByType<ReactionHUD>();

        if (riverScroller == null)
        {
            Debug.LogError(
                "ObstacleCollision: RiverScroller não encontrado!"
            );
        }

        if (reactionHUD == null)
        {
            Debug.LogWarning(
                "ObstacleCollision: ReactionHUD não encontrado!"
            );
        }
    }

    private void ProcessCollision(GameObject other)
    {
        if (alreadyHit)
            return;

        if (other == null)
            return;

        if (!other.CompareTag(playerTag))
            return;

        alreadyHit = true;

        Debug.Log(
            "ObstacleCollision: PLAYER BATEU!"
        );

        // =========================
        // REAÇÃO
        // =========================

        if (reactionHUD != null)
        {
            reactionHUD.ShowBad();
        }

        // =========================
        // CENÁRIO
        // =========================

        if (riverScroller != null)
        {
            Debug.Log(
                "ObstacleCollision: desacelerando cenário."
            );

            riverScroller.SlowDown(
                slowMultiplier,
                slowDuration
            );
        }

        // =========================
        // OBJETOS
        // =========================

        ObstacleMovement[] obstacles =
            FindObjectsByType<ObstacleMovement>(
                FindObjectsSortMode.None
            );

        foreach (ObstacleMovement obstacle in obstacles)
        {
            if (obstacle != null)
            {
                obstacle.SlowDown(
                    slowMultiplier,
                    slowDuration
                );
            }
        }

        // =========================
        // DESTRÓI O OBSTÁCULO
        // =========================

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(
            collision.gameObject
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcessCollision(
            other.gameObject
        );
    }
}