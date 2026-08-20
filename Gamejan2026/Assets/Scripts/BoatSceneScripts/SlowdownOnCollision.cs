using UnityEngine;

public class SlowdownOnCollision : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private ReactionHUD reactionHUD;

    [Header("Desaceleração")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private float duration = 2f;

    [Header("Objeto que causa a desaceleração")]
    [SerializeField] private string targetTag = "sacola";

    private RiverScroller riverScroller;

    private void Start()
    {
        riverScroller =
            FindFirstObjectByType<RiverScroller>();

        if (riverScroller == null)
        {
            Debug.LogWarning(
                "SlowdownOnCollision: RiverScroller não encontrado."
            );
        }

        if (reactionHUD == null)
        {
            Debug.LogError(
                "SlowdownOnCollision: ReactionHUD " +
                "não foi atribuído no Inspector!"
            );
        }
    }

    private void HandleSlow()
    {
        Debug.Log(
            "SlowdownOnCollision: Objeto atingiu o player!"
        );

        if (reactionHUD != null)
        {
            reactionHUD.ShowBad();
        }
        else
        {
            Debug.LogError(
                "SlowdownOnCollision: ReactionHUD está nulo!"
            );
        }

        if (riverScroller != null)
        {
            riverScroller.SlowDown(
                slowMultiplier,
                duration
            );
        }

        // Desacelera todos os objetos que possuem
        // ObstacleMovement.
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
                    duration
                );
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            HandleSlow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(targetTag))
        {
            HandleSlow();
        }
    }
}