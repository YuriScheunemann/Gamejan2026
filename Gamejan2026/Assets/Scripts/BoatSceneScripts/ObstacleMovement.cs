using System.Collections;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Velocidade")]
    [SerializeField] private float speed = 1f;

    private float velocidadeAtual;
    private Coroutine slowdownCoroutine;

    private void Awake()
    {
        velocidadeAtual = speed;

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.bodyType =
                RigidbodyType2D.Kinematic;
        }
    }

    private void Update()
    {
        transform.Translate(
            Vector2.down *
            velocidadeAtual *
            Time.deltaTime,
            Space.World
        );
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        velocidadeAtual = newSpeed;
    }

    public void SlowDown(
        float multiplier,
        float duration)
    {
        if (slowdownCoroutine != null)
        {
            StopCoroutine(
                slowdownCoroutine
            );
        }

        slowdownCoroutine =
            StartCoroutine(
                SlowdownRoutine(
                    multiplier,
                    duration
                )
            );
    }

    private IEnumerator SlowdownRoutine(
        float multiplier,
        float duration)
    {
        float velocidadeOriginal =
            speed;

        velocidadeAtual =
            velocidadeOriginal * multiplier;

        yield return new WaitForSeconds(
            duration
        );

        velocidadeAtual =
            velocidadeOriginal;

        slowdownCoroutine = null;
    }
    public float GetSpeed()
    {
        return velocidadeAtual;
    }
}