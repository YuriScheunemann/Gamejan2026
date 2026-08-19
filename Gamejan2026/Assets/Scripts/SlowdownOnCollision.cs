using System.Collections;
using UnityEngine;

public class SlowdownOnCollision : MonoBehaviour
{
    [Tooltip("Multiplicador aplicado na velocidade do cenário (ex: 0.5 reduz para 50%).")]
    public float slowMultiplier = 0.5f;

    [Tooltip("Duração, em segundos, do efeito de desaceleração.")]
    public float duration = 2f;

    [Tooltip("Tag do objeto que causa a desaceleração.")]
    public string targetTag = "sacola";

    private Coroutine currentRoutine;
    private RiverScroller riverScroller;

    private void Awake()
    {
        riverScroller = FindObjectOfType<RiverScroller>();
        if (riverScroller == null)
        {
            Debug.LogWarning("RiverScroller não encontrado na cena. Atribua manualmente o componente se necessário.");
        }
    }

    private void HandleSlow()
    {
        if (riverScroller == null) return;

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(SlowRoutine());
    }

    private IEnumerator SlowRoutine()
    {
        float original = riverScroller.velocidadeDoCenario;
        riverScroller.velocidadeDoCenario = original * slowMultiplier;

        yield return new WaitForSeconds(duration);

        riverScroller.velocidadeDoCenario = original;
        currentRoutine = null;
    }

    // 2D collisions/triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag)) HandleSlow();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(targetTag)) HandleSlow();
    }

    // 3D collisions/triggers (caso o projeto use física 3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) HandleSlow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(targetTag)) HandleSlow();
    }
}