using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapeCollect : MonoBehaviour
{
    [Header("Cena para carregar")]
    [SerializeField] private string nextSceneName;

    [Header("Tempo antes de trocar")]
    [SerializeField] private float delay = 2f;

    private bool collected = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        Debug.Log("FITA CLICADA!");

        if (collected)
            return;

        collected = true;

        // Esconde a fita
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        // Desativa o collider
        if (col != null)
            col.enabled = false;

        Debug.Log("Iniciando troca de cena em " + delay + " segundos.");

        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Tentando carregar a cena: " + nextSceneName);

        SceneManager.LoadScene(nextSceneName);
    }
}