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

    private int collectedCount = 0;
    private bool canCollectTape = false;
    private bool collectingTape = false;

    // Permite que o ObstacleSpawner saiba se a fita já foi liberada
    public bool CanSpawnTape => canCollectTape;

    private void Start()
    {
        UpdateUI();
    }

    private void CollectFrog(GameObject frog)
    {
        collectedCount++;

        Destroy(frog);

        if (collectedCount >= requiredCount)
        {
            collectedCount = requiredCount;

            // Libera o spawn da fita
            canCollectTape = true;

            Debug.Log("5 sapos coletados! A fita foi liberada.");
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

        Debug.Log("Fita coletada! Mudando de cena em 2 segundos.");

        Invoke(nameof(ChangeScene), delayAfterTape);
    }

    private void ChangeScene()
    {
        int proximaCena =
            SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(proximaCena);
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
        else
        {
            statusText.text =
                "Encontre a fita cassete!";
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