using UnityEngine;
using UnityEngine.UI;

public class TurtleCollector : MonoBehaviour
{
    [Tooltip("Tag dos colecionáveis (ex: 'sapo').")]
    public string collectibleTag = "sapo";

    [Tooltip("Quantidade necessária para ganhar a fita de áudio e ser teleportado ao spawn.")]
    public int requiredCount = 5;

    [Tooltip("Transform do ponto de spawn para onde o player será levado.")]
    public Transform spawnPoint;

    [Header("UI")]
    public Text statusText; // ex: canto da tela: "Sapos: X/5\nFitas: Y"

    private int collectedCount = 0;
    private int audioTapes = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void CollectOne(GameObject collectible)
    {
        collectedCount++;
        // Destrói ou desativa o coletável
        Destroy(collectible);
        UpdateUI();

        if (collectedCount >= requiredCount)
        {
            collectedCount = 0;
            audioTapes++;
            TeleportToSpawn();
            UpdateUI();
        }
    }

    private void TeleportToSpawn()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("SpawnPoint não definido em TurtleCollector.");
        }
    }

    private void UpdateUI()
    {
        if (statusText != null)
        {
            statusText.text = $"Sapos: {collectedCount}/{requiredCount}\nFitas: {audioTapes}";
        }
    }

    // 2D trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(collectibleTag))
        {
            CollectOne(other.gameObject);
        }
    }

    // 3D trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collectibleTag))
        {
            CollectOne(other.gameObject);
        }
    }
}