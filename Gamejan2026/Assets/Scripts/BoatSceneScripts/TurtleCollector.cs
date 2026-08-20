using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurtleCollector : MonoBehaviour
{
    [Header("Sapos")]
    [SerializeField] private string collectibleTag = "sapo";
    [SerializeField] private int requiredCount = 5;

    [Header("Fita Cassete")]
    [SerializeField] private string tapeTag = "fita";
    [SerializeField] private float delayAfterTape = 2f;

    [Header("UI")]
    [SerializeField] private TMP_Text statusText;

    [Header("Reação da Tartaruga")]
    [SerializeField] private ReactionHUD reactionHUD;

    [Header("Cena após coletar a fita")]
    [SerializeField] private string sceneName;

    private int collectedCount = 0;
    private bool canCollectTape = false;
    private bool collectingTape = false;

    public bool CanSpawnTape => canCollectTape;

    private void Start()
    {
        UpdateUI();

        if (reactionHUD == null)
        {
            Debug.LogError(
                "TurtleCollector: ReactionHUD não foi atribuído no Inspector!"
            );
        }
    }

    private void CollectFrog(GameObject frog)
    {
        if (canCollectTape)
            return;

        collectedCount++;

        Debug.Log(
            $"TurtleCollector: Sapo coletado! " +
            $"{collectedCount}/{requiredCount}"
        );

        if (reactionHUD != null)
        {
            reactionHUD.ShowGood();
        }
        else
        {
            Debug.LogError(
                "TurtleCollector: ReactionHUD está nulo!"
            );
        }

        Destroy(frog);

        if (collectedCount >= requiredCount)
        {
            collectedCount = requiredCount;

            canCollectTape = true;

            Debug.Log(
                "Todos os sapos foram coletados! " +
                "A fita foi liberada."
            );
        }

        UpdateUI();
    }

    private void CollectTape(GameObject tape)
    {
        if (!canCollectTape)
            return;

        if (collectingTape)
            return;

        collectingTape = true;

        Destroy(tape);

        Debug.Log(
            "Fita coletada! Mudando de cena em 2 segundos."
        );

        UpdateUI();

        Invoke(
            nameof(ChangeScene),
            delayAfterTape
        );
    }

    private void ChangeScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError(
                "TurtleCollector: Nome da cena não foi definido!"
            );

            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void UpdateUI()
    {
        if (statusText == null)
            return;

        if (!canCollectTape)
        {
            statusText.text =
                $"Sapos: {collectedCount}/{requiredCount}";
        }
        else if (!collectingTape)
        {
            statusText.text =
                "Encontre a fita cassete!";
        }
        else
        {
            statusText.text =
                "Fita coletada!";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(collectibleTag))
        {
            CollectFrog(other.gameObject);
            return;
        }

        if (other.CompareTag(tapeTag))
        {
            CollectTape(other.gameObject);
        }
    }
}