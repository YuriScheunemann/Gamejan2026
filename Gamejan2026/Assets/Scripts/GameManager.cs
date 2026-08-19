using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Referências")]
    [SerializeField] private RiverScroller riverScroller;

    [Header("Dano / Sacolas")]
    [SerializeField] private int maxHits = 3;
    [SerializeField] private float slowMultiplier = 0.5f; // velocidade enquanto está "danificado"
    [SerializeField] private float slowDuration = 2f;

    [Header("Tartarugas")]
    [SerializeField] private int turtlesToWin = 5;

    private int currentHits;
    private int turtlesCollected;
    private float normalRiverSpeed;
    private Coroutine slowCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (riverScroller != null)
            normalRiverSpeed = riverScroller.velocidadeDoCenario;
        else
            normalRiverSpeed = 1f; // fallback
    }

    public void HitPipe()
    {
        Debug.Log("Colidiu com cano -> Game Over");
        GameOver();
    }

    public void HitBag()
    {
        currentHits++;
        Debug.Log($"Acertou sacola ({currentHits}/{maxHits})");
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        slowCoroutine = StartCoroutine(ApplySlowCoroutine());

        if (currentHits >= maxHits)
            GameOver();
    }

    private IEnumerator ApplySlowCoroutine()
    {
        if (riverScroller != null)
            riverScroller.velocidadeDoCenario = normalRiverSpeed * slowMultiplier;

        yield return new WaitForSeconds(slowDuration);

        if (riverScroller != null)
            riverScroller.velocidadeDoCenario = normalRiverSpeed;

        slowCoroutine = null;
    }

    public void CollectTurtle()
    {
        turtlesCollected++;
        Debug.Log($"Tartarugas coletadas: {turtlesCollected}/{turtlesToWin}");
        if (turtlesCollected >= turtlesToWin)
            Win();
    }

    private void GameOver()
    {
        // Comportamento de Game Over: log e tentativa de abrir cena "GameOver" se existir.
        Debug.Log("GAME OVER");
        try
        {
            SceneManager.LoadScene("GameOver");
        }
        catch
        {
            // Se não houver cena chamada "GameOver", apenas reinicia a cena atual como fallback
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Win()
    {
        Debug.Log("VOCÊ GANHOU! Coletou todas as tartarugas.");
        try
        {
            SceneManager.LoadScene("Win");
        }
        catch
        {
            // fallback: recarrega cena atual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}