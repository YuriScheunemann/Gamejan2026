using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    [Header("Tags que podem ser destruídas")]
    [SerializeField]
    private string[] tagsParaDestruir =
    {
        "sapo",
        "fita",
        "obstaculo"
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in tagsParaDestruir)
        {
            if (other.CompareTag(tag))
            {
                Destroy(other.gameObject);
                return;
            }
        }
    }
}
