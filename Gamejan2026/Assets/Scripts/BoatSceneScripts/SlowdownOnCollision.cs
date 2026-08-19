using System.Collections;
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
        riverScroller = FindFirstObjectByType<RiverScroller>();

        if (riverScroller == null)
        {
            Debug.LogWarning(
                "SlowdownOnCollision: RiverScroller não encontrado."
            );
        }
    }

    private void HandleSlow()
    {
        if (riverScroller == null)
            return;

        reactionHUD?.ShowBad();

        riverScroller.SlowDown(
            slowMultiplier,
            duration
        );
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